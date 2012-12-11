// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChargedMinersLauncher {
    public sealed partial class MainForm : Form {
        static MainForm instance;
        #region Startup / Shutdown

        public MainForm() {
            instance = this;

            // Start logging
            if( File.Exists( Paths.LauncherLogPath ) ) {
                File.Delete( Paths.LauncherLogPath + ".old" );
                File.Move( Paths.LauncherLogPath, Paths.LauncherLogPath + ".old" );
            }
            Log( "---- " + DateTime.Now.ToLongDateString() + " ----" );
            Log( "Charged-Miners Launcher 1.13 dev" );

            // Set up the GUI
            InitializeComponent();
            SetToolTips();
            lOptionsSaved.Text = "";
            lToolStatus.Text = "";

            // hook up event handlers
            signInWorker.DoWork += SignIn;
            signInWorker.RunWorkerCompleted += OnSignInCompleted;

            versionCheckWorker.DoWork += CheckUpdates;
            versionCheckWorker.RunWorkerCompleted += OnUpdateCheckCompleted;

            binaryDownloader.DownloadProgressChanged += OnDownloadProgress;
            binaryDownloader.DownloadFileCompleted += OnDownloadCompleted;

            Shown += OnShown;
            FormClosed += OnFormClosed;

            // load and apply settings
            LoadLauncherSettings();

            switch( (StartingTab)cStartingTab.SelectedIndex ) {
                case StartingTab.Direct:
                    tabs.SelectedTab = tabDirect;
                    break;
                case StartingTab.Resume:
                    tabs.SelectedTab = tabResume;
                    break;
                case StartingTab.SignIn:
                    tabs.SelectedTab = tabSignIn;
                    break;
            }
        }


        bool settingsLoaded;


        void LoadLauncherSettings() {
            Log( "LoadLauncherSettings" );
            SettingsFile settings = new SettingsFile();
            if( File.Exists( Paths.LauncherSettingsPath ) ) {
                settings.Load( Paths.LauncherSettingsPath );
            }
            bool saveUsername = settings.GetBool( "rememberUsername", true );
            bool multiUser = settings.GetBool( "multiUser", false );
            bool savePassword = settings.GetBool( "rememberPassword", false );
            bool saveUrl = settings.GetBool( "rememberServer", true );
            GameUpdateMode gameUpdateMode = settings.GetEnum( "gameUpdateMode", GameUpdateMode.Ask );
            StartingTab startingTab = settings.GetEnum( "startingTab", StartingTab.SignIn );
            xRememberUsername.Checked = saveUsername;
            xMultiUser.Checked = multiUser;
            xRememberPassword.Checked = savePassword;
            xRememberServer.Checked = saveUrl;
            cGameUpdates.SelectedIndex = (int)gameUpdateMode;
            cStartingTab.SelectedIndex = (int)startingTab;
            settingsLoaded = true;
        }


        const string UnsupportedPlatformHeader = "Unsupported platform.",
                     UnsupportedPlatformText = "Charged-Miners is only available for Windows, Linux, and MacOS X.";


        void OnShown( object sender, EventArgs e ) {
            if( !Paths.IsPlatformSupported ) {
                State = FormState.UnrecoverableError;
                lStatus.Text = UnsupportedPlatformHeader;
                lStatus2.Text = UnsupportedPlatformText;
                return;
            }

            LoadLoginInfo();

            // trigger input validation
            SignInFieldChanged( xSignInUsername, EventArgs.Empty );
            tDirectUrl_TextChanged( tDirectUrl, EventArgs.Empty );

            State = FormState.AtMainForm;

            // Load "Resume" information from CM's settings file
            if( File.Exists( Paths.GameSettingsPath ) ) {
                SettingsFile gameSettings = new SettingsFile();
                gameSettings.Load( Paths.GameSettingsPath );
                string resumeUri = gameSettings.GetString( "mc.lastMcUrl", "" );
                if( resumeUri.Length > 0 ) {
                    Match match = PlayLinkDirect.Match( resumeUri );
                    if( match.Success ) {
                        tResumeUri.Text = resumeUri;
                        tResumeServerIP.Text = match.Groups[1].Value;
                        tResumeUsername.Text = match.Groups[7].Value;
                        string resumeServerName = gameSettings.GetString( "mc.lastClassicServer", "" );
                        if( resumeServerName.Length > 0 ) {
                            tResumeServerName.Text = resumeServerName;
                        } else {
                            tResumeServerName.Text = "?";
                        }
                        bResume.Enabled = true;
                    } else {
                        tResumeUri.Text = "(Not Available)";
                        tResumeUsername.Text = "?";
                        tResumeServerIP.Text = "?";
                        tResumeServerName.Text = "?";
                    }
                }
            }

            // To fix compatibility with Lighttpd when doing HttpWebRequests
            ServicePointManager.Expect100Continue = false;
            // To bypass HTTPS certificate validation
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };

            if( (GameUpdateMode)cGameUpdates.SelectedIndex == GameUpdateMode.Never ) {
                updateCheckCompleted = true;
            } else {
                versionCheckWorker.RunWorkerAsync();
            }
        }


        ToolTip toolTip;

        const string ToolTipSignInUsername = "Your minecraft.net username or email",
                     ToolTipSignInPassword = "Your minecraft.net password",
                     ToolTipSignInUrl = "Server's Minecraft.net URL or a DirectConnect (mc://) link. Optional.",
                     ToolTipRememberUsername = "Save your username for next time.",
                     ToolTipRememberPassword =
                         "Save your password for next time. Note that password is stored in plain text.",
                     ToolTipDirectUrl = "Server's DirectConnect (mc://) link.",
                     ToolTipResume =
                         "Try to reuse the last-used credentials, to connect to the most-recently-joined server.",
                     ToolTipResetSettings = "Resets Charged-Miners and Launcher settings to defaults.",
                     ToolTipDeleteData = "Deletes all private data: settings, logs, stored login data, etc.",
                     ToolTipOpenDataDir = "Opens up Charged-Miners' data directory.",
                     ToolTipUploadLog =
                         "Uploads your Charged-Miners and launcher log files, which contain debugging information, for easy sharing.";


        void SetToolTips() {
            toolTip = new ToolTip();
            toolTip.SetToolTip( xSignInUsername, ToolTipSignInUsername );
            toolTip.SetToolTip( lSignInUsername, ToolTipSignInUsername );
            toolTip.SetToolTip( tSignInPassword, ToolTipSignInPassword );
            toolTip.SetToolTip( lSignInPassword, ToolTipSignInPassword );
            toolTip.SetToolTip( tSignInUrl, ToolTipSignInUrl );
            toolTip.SetToolTip( lSignInUrl, ToolTipSignInUrl );

            toolTip.SetToolTip( bResume, ToolTipResume );

            toolTip.SetToolTip( tDirectUrl, ToolTipDirectUrl );
            toolTip.SetToolTip( lDirectUrl, ToolTipDirectUrl );

            toolTip.SetToolTip( xRememberUsername, ToolTipRememberUsername );
            toolTip.SetToolTip( xRememberPassword, ToolTipRememberPassword );

            toolTip.SetToolTip( bResetSettings, ToolTipResetSettings );
            toolTip.SetToolTip( bDeleteData, ToolTipDeleteData );
            toolTip.SetToolTip( bOpenDataDir, ToolTipOpenDataDir );
            toolTip.SetToolTip( bUploadLog, ToolTipUploadLog );
        }


        // log close reason
        static void OnFormClosed( object sender, FormClosedEventArgs e ) {
            Log( "Closed: " + e.CloseReason );
        }

        #endregion


        #region UI Event Hooks

        // ==== Sign-In tab ====
        void SignInFieldChanged( object sender, EventArgs e ) {
            lSignInStatus.Text = "";

            bool canSignIn = false;
            // check the username field
            if( UsernameRegex.IsMatch( xSignInUsername.Text ) || EmailRegex.IsMatch( xSignInUsername.Text ) ) {
                xSignInUsername.BackColor = SystemColors.Window;
                canSignIn = true;
            } else {
                xSignInUsername.BackColor = Color.Yellow;
                lSignInStatus.Text = "Invalid username/email.";
            }

            // check the password field
            if( tSignInPassword.Text.Length == 0 ) {
                canSignIn = false;
                tSignInPassword.BackColor = Color.Yellow;
                if( sender == tSignInPassword || lSignInStatus.Text.Length == 0 ) {
                    lSignInStatus.Text = "Password is required.";
                }
            } else {
                tSignInPassword.BackColor = SystemColors.Window;
            }

            // check the URL field
            Match match = PlayLinkDirect.Match( tSignInUrl.Text );
            if( match.Success ) {
                // "mc://" url
                if( match.Groups[7].Value.Equals( xSignInUsername.Text, StringComparison.OrdinalIgnoreCase ) ) {
                    tSignInUrl.BackColor = SystemColors.Window;
                    launchMode = LaunchMode.SignInWithUri;
                } else {
                    canSignIn = false;
                    tSignInUrl.BackColor = Color.Yellow;
                    if( sender == tSignInUrl || lSignInStatus.Text.Length == 0 ) {
                        lSignInStatus.Text = "Given sign-in username does not match username in direct-connect URL.";
                    }
                }

            } else if( PlayLinkHash.IsMatch( tSignInUrl.Text ) || PlayLinkIPPort.IsMatch( tSignInUrl.Text ) ) {
                // minecraft.net play link
                launchMode = LaunchMode.SignInWithUri;
                tSignInUrl.BackColor = SystemColors.Window;

            } else if( tSignInUrl.Text.Length == 0 ) {
                // no URL given
                launchMode = LaunchMode.SignIn;
                tSignInUrl.BackColor = SystemColors.Window;

            } else {
                // unrecognized URL given
                tSignInUrl.BackColor = Color.Yellow;
                if( sender == tSignInUrl || lSignInStatus.Text.Length == 0 ) {
                    lSignInStatus.Text = "Unrecognized URL";
                }
                canSignIn = false;
            }

            bSignIn.Enabled = canSignIn;
        }


        void bSignIn_Click( object sender, EventArgs e ) {
            Log( "[SignIn]" );
            string minecraftUsername;
            if( xRememberUsername.Checked && xSignInUsername.Text == storedLoginUsername ) {
                minecraftUsername = storedMinecraftUsername;
            } else {
                minecraftUsername = xSignInUsername.Text;
            }
            signInSession = new MinecraftNetSession( xSignInUsername.Text, minecraftUsername, tSignInPassword.Text );

            State = FormState.SigningIn;
            signInWorker.RunWorkerAsync();
        }


        // ==== Resume tab ====
        void bResume_Click( object sender, EventArgs e ) {
            Log( "[Resume]" );
            launchMode = LaunchMode.Resume;
            loginCompleted = true;
            if( updateCheckCompleted ) {
                OnSignInAndUpdateCheckCompleted();
            } else {
                State = FormState.WaitingForUpdater;
            }
        }


        // ==== Direct tab ====
        void tDirectUrl_TextChanged( object sender, EventArgs e ) {
            // check the URL field
            Match match = PlayLinkDirect.Match( tDirectUrl.Text );
            if( match.Success ) {
                // Acceptable "mc://" url
                tDirectServerIP.Text = match.Groups[1].Value;
                tDirectUsername.Text = match.Groups[7].Value;
                tDirectUrl.BackColor = SystemColors.Window;
                launchMode = LaunchMode.Direct;
                lDirectStatus.Text = "";
                bDirectConnect.Enabled = true;

            } else {
                // Unacceptable or missing URL
                tDirectServerIP.Text = "?";
                tDirectUsername.Text = "?";
                tDirectUrl.BackColor = Color.Yellow;
                bDirectConnect.Enabled = false;
                if( PlayLinkHash.IsMatch( tDirectUrl.Text ) || PlayLinkIPPort.IsMatch( tDirectUrl.Text ) ) {
                    // minecraft.net play link
                    lDirectStatus.Text = "You must sign in to connect to servers via minecraft.net links.";
                } else if( tDirectUrl.Text.Length == 0 ) {
                    // no URL given
                    lDirectStatus.Text = "Provide a direct-connect (mc://) URL.";
                } else {
                    // unrecognized URL given
                    lDirectStatus.Text = "Unrecognized URL.";
                }
            }
        }


        void bDirectConnect_Click( object sender, EventArgs e ) {
            Log( "[DirectConnect]" );
            launchMode = LaunchMode.Direct;
            loginCompleted = true;
            if( updateCheckCompleted ) {
                OnSignInAndUpdateCheckCompleted();
            } else {
                State = FormState.WaitingForUpdater;
            }
        }


        // ==== Options tab ====
        void SaveLauncherSettings( object sender, EventArgs e ) {
            if( !settingsLoaded ) return;
            Log( "SaveLauncherSettings" );
            SettingsFile settings = new SettingsFile();
            settings.Set( "rememberUsername", xRememberUsername.Checked );
            settings.Set( "multiUser", xMultiUser.Checked );
            settings.Set( "rememberPassword", xRememberPassword.Checked );
            settings.Set( "rememberServer", xRememberServer.Checked );
            settings.Set( "gameUpdateMode", (GameUpdateMode)cGameUpdates.SelectedIndex );
            settings.Set( "startingTab", (StartingTab)cStartingTab.SelectedIndex );
            settings.Save( Paths.LauncherSettingsPath );

            if( sender == xRememberUsername ) {
                lOptionsSaved.Text = "\"Remember username\" preference saved.";
            } else if( sender == xMultiUser ) {
                lOptionsSaved.Text = "\"Multiple users\" preference saved.";
            } else if( sender == xRememberPassword ) {
                lOptionsSaved.Text = "\"Remember password\" preference saved.";
            } else if( sender == xRememberServer ) {
                lOptionsSaved.Text = "\"Remember server\" preference saved.";
            } else if( sender == cGameUpdates ) {
                lOptionsSaved.Text = "\"Game updates\" preference saved.";
            } else if( sender == cStartingTab ) {
                lOptionsSaved.Text = "\"Starting tab\" preference saved.";
            } else {
                lOptionsSaved.Text = "Preferences saved.";
            }
        }


        // ==== Tools tab ====
        void bResetSettings_Click( object sender, EventArgs e ) {
            Log( "[ResetSettings]" );
            File.Delete( Paths.LauncherSettingsPath );
            File.Delete( Paths.GameSettingsPath );
            settingsLoaded = false;
            LoadLauncherSettings();
            lToolStatus.Text = "Launcher and game settings were reset to defaults.";
        }


        void bDeleteData_Click( object sender, EventArgs e ) {
            Log( "[DeleteData]" );
            File.Delete( Paths.LauncherSettingsPath );
            File.Delete( Paths.GameSettingsPath );
            File.Delete( Paths.LauncherLogPath );
            File.Delete( Paths.GameLogPath );
            File.Delete( Path.Combine( Paths.DataPath, Paths.CookieContainerFile ) );
            File.Delete( Path.Combine( Paths.DataPath, Paths.PasswordSaveFile ) );
            settingsLoaded = false;
            LoadLauncherSettings();
            lToolStatus.Text = "All settings, logs, and saved data were deleted.";
        }


        void bOpenDataDir_Click( object sender, EventArgs e ) {
            Log( "[OpenDataDir]" );
            Process.Start( "file://" + Paths.DataPath );
        }


        void bUploadLog_Click( object sender, EventArgs e ) {
            Log( "[UploadLog]" );
            JsonObject files = new JsonObject();
            if( File.Exists( Paths.GameLogPath ) ) {
                string content = File.ReadAllText( Paths.GameLogPath );
                files.Add( "log.txt",
                           new JsonObject { { "content", content } } );
            }
            if( File.Exists( Paths.LauncherLogPath ) ) {
                string content = File.ReadAllText( Paths.LauncherLogPath );
                files.Add( "launcher.log",
                           new JsonObject { { "content", content } } );
            }
            if( File.Exists( Paths.LauncherLogPath + ".old" ) ) {
                string content = File.ReadAllText( Paths.LauncherLogPath + ".old" );
                files.Add( "launcher.log.old",
                           new JsonObject { { "content", content } } );
            }
            if( files.Count == 0 ) {
                lToolStatus.Text = "No log files to submit";
                return;
            }

            JsonObject request = new JsonObject {
                { "description", "Charged-Miners log upload" },
                { "public", false },
                { "files", files }
            };
            WebClient logClient = new WebClient();
            string dataString = request.Serialize();
            byte[] data = Encoding.UTF8.GetBytes( dataString );
            try {
                byte[] responseData = logClient.UploadData( "https://api.github.com/gists", data );
                string responseString = Encoding.UTF8.GetString( responseData );
                JsonObject response = new JsonObject( responseString );
                tPastebinUrl.Text = response.GetString( "html_url" );
                tPastebinUrl.Select();
                tPastebinUrl.SelectAll();
                lToolStatus.Text = "Log files uploaded! Please copy the given URL.";
            } catch( WebException ex ) {
                Log( "UploadLog ERROR: " + ex );
                lToolStatus.Text = "Log file upload failed! " + ex.Message;
            }
        }

        #endregion


        #region Update Check / Update Download

        readonly BackgroundWorker versionCheckWorker = new BackgroundWorker();
        string localHashString;
        static readonly Uri UpdateUri = new Uri( "http://cloud.github.com/downloads/Charged/Miners/" );
        readonly WebClient binaryDownloader = new WebClient();
        VersionInfo latestVersion;
        Exception updaterException;


        void CheckUpdates( object sender, DoWorkEventArgs e ) {
            try {
                Log( "CheckUpdates" );
                VersionsTxt versionList;

                // download and parse version.txt
                using( WebClient updateCheckDownloader = new WebClient() ) {
                    string versions = updateCheckDownloader.DownloadString( UpdateUri + "version.txt" );
                    versionList = new VersionsTxt( versions.Split( '\n' ) );
                }

                // check if primary binary download is available
                latestVersion = versionList.Get( Paths.PrimaryBinary );
                if( latestVersion != null ) {
                    // check if local primary binary exists
                    if( File.Exists( Paths.PrimaryBinary ) ) {
                        localHashString = ComputeLocalHash( Paths.PrimaryBinary );
                    } // else download primary binary
                    return;
                }

                // no alternative available, fail
                if( Paths.AlternativeBinary == null ) return;

                // check if alternative binary download is available
                latestVersion = versionList.Get( Paths.AlternativeBinary );
                if( latestVersion != null ) {
                    // check if local alt binary exists
                    if( File.Exists( Paths.AlternativeBinary ) ) {
                        localHashString = ComputeLocalHash( Paths.AlternativeBinary );
                    } // else download alt binary
                }
            } catch( Exception ex ) {
                updaterException = ex;
                Log( "CheckUpdates ERROR: " + ex );
            }
        }


        // returns MD5 of a given file, as a hexadecimal string
        static string ComputeLocalHash( string fileName ) {
            using( MD5 hasher = MD5.Create() ) {
                using( FileStream fs = File.OpenRead( fileName ) ) {
                    StringBuilder sb = new StringBuilder( 32 );
                    foreach( byte b in hasher.ComputeHash( fs ) ) {
                        sb.AppendFormat( "{0:x2}", b );
                    }
                    return sb.ToString();
                }
            }
        }


        void OnUpdateCheckCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            Log( "OnUpdateCheckCompleted" );
            updateCheckCompleted = true;

            if( latestVersion != null &&
                ( !File.Exists( latestVersion.Name ) ||
                  !latestVersion.Md5.Equals( localHashString, StringComparison.OrdinalIgnoreCase ) ) ) {
                DownloadBegin();
            }

            if( loginCompleted ) OnSignInAndUpdateCheckCompleted();
        }


        void DownloadBegin() {
            Log( "DownloadBegin" );
            binaryDownloader.DownloadFileAsync( new Uri( UpdateUri + latestVersion.HttpName ),
                                                latestVersion.Name + ".tmp" );
        }


        void OnDownloadProgress( object sender, DownloadProgressChangedEventArgs e ) {
            pbSigningIn.Value = e.ProgressPercentage;
            if( State == FormState.DownloadingBinary ) {
                lStatus.Text = String.Format( "Downloading {0} ({1}/{2})",
                                              latestVersion.HttpName,
                                              e.BytesReceived,
                                              e.TotalBytesToReceive );
            }
        }


        void OnDownloadCompleted( object sender, AsyncCompletedEventArgs e ) {
            Log( "OnDownloadCompleted" );
            if( e.Cancelled ) return;
            downloadComplete = true;
            if( State == FormState.DownloadingBinary ) {
                ApplyUpdate();
            }
        }


        void ApplyUpdate() {
            Log( "ApplyUpdate" );
            if( File.Exists( latestVersion.Name ) ) {
                File.Delete( latestVersion.Name + ".old" );
                File.Replace( latestVersion.Name + ".tmp", latestVersion.Name, latestVersion.Name + ".old", true );
            } else {
                File.Move( latestVersion.Name + ".tmp", latestVersion.Name );
            }
            StartChargedMiners();
        }

        #endregion


        #region Sign-In

        MinecraftNetSession signInSession;
        LaunchMode launchMode;

        string storedLoginUsername,
               storedMinecraftUsername;

        readonly BackgroundWorker signInWorker = new BackgroundWorker();

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@" +
                                    @"(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                                    RegexOptions.IgnoreCase ),
            PlayLinkHash =
                new Regex( @"^http://(www\.)?minecraft.net/classic/play/([0-9a-fA-F]{28,32})/?(\?override=(true|1))?$" ),
            PlayLinkDirect =
                new Regex( @"^mc://((localhost|(\d{1,3}\.){3}\d{1,3}|([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-]+))(:\d{1,5})?)/([a-zA-Z0-9_\.]{2,16})/(.*)$" ),
            PlayLinkIPPort =
                new Regex( @"^http://(www\.)?minecraft.net/classic/play\?ip=(localhost|(\d{1,3}\.){3}\d{1,3}|([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-]+))&port=(\d{1,5})$" );


        void SignIn( object sender, DoWorkEventArgs e ) {
            signInSession.Login( xRememberUsername.Checked && xRememberPassword.Checked );
        }


        void OnSignInCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            if( e.Error != null ) {
                signInSession.Status = LoginResult.Error;
            }
            Log( "OnSignInCompleted " + signInSession.Status );
            switch( signInSession.Status ) {
                case LoginResult.Success:
                    SaveLoginInfo();
                    loginCompleted = true;
                    if( updateCheckCompleted ) {
                        OnSignInAndUpdateCheckCompleted();
                    } else {
                        State = FormState.WaitingForUpdater;
                    }
                    break;

                case LoginResult.MigratedAccount:
                    State = FormState.AtMainForm;
                    lSignInStatus.Text = "Migrated account. Use your email to sign in.";
                    xSignInUsername.Select();
                    break;

                case LoginResult.WrongUsernameOrPass:
                    State = FormState.AtMainForm;
                    lSignInStatus.Text = "Wrong username or password.";
                    tSignInPassword.Select();
                    break;

                case LoginResult.UnrecognizedResponse:
                    State = FormState.AtMainForm;
                    lSignInStatus.Text = "Could not understand minecraft.net response.";
                    break;

                case LoginResult.NoPlaySession:
                    State = FormState.AtMainForm;
                    lSignInStatus.Text = "Could not start a play session.";
                    break;

                case LoginResult.Error:
                    State = FormState.AtMainForm;
                    if( e.Error != null ) {
                        Log( "LoginException: " + e.Error );
                        lSignInStatus.Text = "Error: " + e.Error.Message;
                    } else {
                        lSignInStatus.Text = "An unknown error occurred.";
                    }
                    break;

                case LoginResult.Canceled:
                    State = FormState.AtMainForm;
                    lSignInStatus.Text = "Sign-in canceled.";
                    return;
            }
        }


        void LoadLoginInfo() {
            try {
                if( File.Exists( Paths.PasswordSaveFile ) ) {
                    string[] loginData = File.ReadAllLines( Paths.PasswordSaveFile );
                    if( xRememberUsername.Checked ) {
                        storedLoginUsername = loginData[0];
                        xSignInUsername.Text = storedLoginUsername;
                        storedMinecraftUsername = loginData.Length > 2 ? loginData[2] : storedLoginUsername;
                    }
                    if( xRememberPassword.Checked ) {
                        tSignInPassword.Text = loginData[1];
                    }
                    if( xRememberServer.Checked ) {
                        tSignInUrl.Text = loginData.Length > 3 ? loginData[3] : "";
                    }
                }
            } catch( Exception ) {
                lSignInStatus.Text = "Could not load saved login information.";
            }
        }


        void SaveLoginInfo() {
            if( xRememberUsername.Checked || xRememberPassword.Checked || xRememberServer.Checked ) {
                File.WriteAllLines( Paths.PasswordSaveFile, new[] {
                    xRememberUsername.Checked ? signInSession.LoginUsername : "",
                    xRememberPassword.Checked ? signInSession.Password : "",
                    xRememberUsername.Checked ? signInSession.MinercraftUsername : "",
                    xRememberServer.Checked ? tSignInUrl.Text : ""
                } );
            } else {
                if( File.Exists( Paths.PasswordSaveFile ) ) {
                    File.Delete( Paths.PasswordSaveFile );
                }
            }
        }

        #endregion


        #region State Control

        volatile bool loginCompleted,
                      updateCheckCompleted,
                      downloadComplete;


        void OnSignInAndUpdateCheckCompleted() {
            Log( "OnSignInAndUpdateCheckCompleted" );

            if( latestVersion == null ) {
                // Updater failed!
                if( File.Exists( Paths.PrimaryBinary ) || File.Exists( Paths.AlternativeBinary ) ) {
                    // Start whatever we have locally
                    StartChargedMiners();
                } else {
                    // no CM version found for this platform; notify, then terminate
                    State = FormState.UnrecoverableError;
                    lStatus.Text = "Could not download Charged-Miners!";
                    if( updaterException != null ) {
                        lStatus2.Text = updaterException.GetType().Name + Environment.NewLine + updaterException.Message;
                    } else {
                        lStatus2.Text = "Unknown error occured, or no binaries are available for your platform.";
                    }
                }
                return;
            }

            // no local CM binaries: download them
            if( !File.Exists( latestVersion.Name ) ) {
                if( downloadComplete ) {
                    ApplyUpdate();
                } else {
                    State = FormState.DownloadingBinary;
                }

            } else if( latestVersion != null &&
                       !latestVersion.Md5.Equals( localHashString, StringComparison.OrdinalIgnoreCase ) ) {
                // local CM binaries are outdated
                switch( (GameUpdateMode)cGameUpdates.SelectedIndex ) {
                    case GameUpdateMode.Always:
                        ApplyUpdate(); // update automatically
                        break;
                    case GameUpdateMode.Ask:
                        State = FormState.PromptingToUpdate; // prompt
                        break;
                    case GameUpdateMode.Never:
                        StartChargedMiners(); // skip update
                        break;
                }
            } else {
                // local CM binaries are up to date; run
                StartChargedMiners();
            }
        }


        FormState State {
            get { return state; }
            set {
                Log( "State = " + value );
                switch( value ) {
                    case FormState.AtMainForm:
                        CancelButton = null;
                        lStatus.Text = "";
                        lStatus2.Text = "";
                        SelectedPanel = tabs;
                        tabs_SelectedIndexChanged( tabs, EventArgs.Empty );
                        break;

                    case FormState.SigningIn:
                        AcceptButton = null;
                        CancelButton = bCancel;
                        lSignInStatus.Text = "";
                        lStatus.Text = String.Format( "Signing in as {0}...",
                                                      signInSession.LoginUsername );
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Marquee;
                        bCancel.Text = "Cancel";
                        bCancel.Visible = true;
                        SelectedPanel = panelStatus;
                        break;

                    case FormState.WaitingForUpdater:
                        AcceptButton = null;
                        CancelButton = bCancel;
                        lSignInStatus.Text = "";
                        lStatus.Text = "Checking for updates...";
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Marquee;
                        bCancel.Visible = false;
                        break;

                    case FormState.UnrecoverableError:
                        AcceptButton = bCancel;
                        CancelButton = null;
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 100;
                        bCancel.Text = "OK";
                        bCancel.Visible = true;
                        break;

                    case FormState.PromptingToUpdate:
                        AcceptButton = bUpdateYes;
                        CancelButton = bUpdateNo;
                        SelectedPanel = panelUpdatePrompt;
                        break;

                    case FormState.DownloadingBinary:
                        AcceptButton = null;
                        CancelButton = bCancel;
                        lStatus.Text = String.Format( "Downloading {0} (?/?)", latestVersion.HttpName );
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 0;
                        SelectedPanel = panelStatus;
                        bCancel.Text = "Cancel";
                        bCancel.Visible = true;
                        break;
                }
                state = value;
            }
        }

        FormState state;


        Control SelectedPanel {
            set {
                if( value == tabs ) {
                    panelStatus.Visible = false;
                    panelUpdatePrompt.Visible = false;
                    tabs.Visible = true;
                    tabs_SelectedIndexChanged( tabs, EventArgs.Empty );
                } else if( value == panelStatus ) {
                    panelUpdatePrompt.Visible = false;
                    tabs.Visible = false;
                    panelStatus.Visible = true;
                } else {
                    tabs.Visible = false;
                    panelStatus.Visible = false;
                    panelUpdatePrompt.Visible = true;
                }
            }
        }


        void tabs_SelectedIndexChanged( object sender, EventArgs e ) {
            if( tabs.Visible ) {
                if( tabs.SelectedTab == tabSignIn ) {
                    AcceptButton = bSignIn;
                    if( xSignInUsername.Text.Length == 0 ) {
                        xSignInUsername.Focus();
                    } else if( tSignInPassword.Text.Length == 0 ) {
                        tSignInPassword.Focus();
                    } else {
                        tSignInUrl.Focus();
                    }
                } else if( tabs.SelectedTab == tabResume ) {
                    AcceptButton = bResume;
                    bResume.Focus();
                } else if( tabs.SelectedTab == tabDirect ) {
                    AcceptButton = bDirectConnect;
                    tDirectUrl.Focus();
                } else {
                    AcceptButton = null;
                    if( tabs.SelectedTab == tabTools ) {
                        SettingsFile sf = new SettingsFile();
                        if( File.Exists( Paths.GameSettingsPath ) ) {
                            sf.Load( Paths.GameSettingsPath );
                        }
                        xFailSafe.Checked = sf.GetBool( "mc.failsafe", false );
                    }
                }
            }
        }


        void bUpdateYes_Click( object sender, EventArgs e ) {
            if( downloadComplete ) {
                Log( "[UpdateYes] Applying" );
                ApplyUpdate();
            } else {
                Log( "[UpdateYes] Downloading" );
                State = FormState.DownloadingBinary;
            }
        }


        void bUpdateNo_Click( object sender, EventArgs e ) {
            Log( "[UpdateNo]" );
            binaryDownloader.CancelAsync();
            StartChargedMiners();
        }


        void bCancel_Click( object sender, EventArgs e ) {
            Log( "[Cancel] " + State );
            switch( State ) {
                case FormState.SigningIn:
                    signInSession.CancelAsync();
                    lStatus2.Text = "Canceling...";
                    break;
                case FormState.UnrecoverableError:
                    Environment.ExitCode = 1;
                    Application.Exit();
                    break;
                case FormState.DownloadingBinary:
                    binaryDownloader.CancelAsync();
                    StartChargedMiners();
                    break;
            }
        }

        #endregion


        internal static void SetStatus( string text ) {
            if( instance.lStatus2.InvokeRequired ) {
                instance.lStatus2.BeginInvoke( (MethodInvoker)delegate {
                    instance.lStatus2.Text = text;
                } );
            } else {
                instance.lStatus2.Text = text;
            }
        }


        void StartChargedMiners() {
            lStatus.Text = "Launching Charged-Miners...";
            bCancel.Visible = false;
            Log( "StartChargedMiners" );

            // determine which binary to call
            string binaryFileName;
            if( File.Exists( Paths.PrimaryBinary ) ) {
                binaryFileName = Paths.PrimaryBinary;
            } else {
                binaryFileName = Paths.AlternativeBinary;
            }

            // if we are on unix, set +x on Charge binaries
            if( RuntimeInfo.IsUnix ) {
                Process chmod = new Process {
                    StartInfo = {
                        FileName = "chmod",
                        Arguments = "a+x " + binaryFileName,
                        UseShellExecute = true
                    }
                };
                chmod.Start();
                chmod.WaitForExit();
            }

            // build CM parameter string
            string param;
            switch( launchMode ) {
                case LaunchMode.Direct:
                    param = tDirectUrl.Text;
                    break;
                case LaunchMode.Resume:
                    param = tResumeUri.Text;
                    break;
                case LaunchMode.SignIn:
                    param = "PLAY_SESSION=" + signInSession.PlaySessionCookie.Value;
                    break;
                case LaunchMode.SignInWithUri:
                    param = String.Format( "PLAY_SESSION={0} {1}",
                                           signInSession.PlaySessionCookie.Value,
                                           tSignInUrl.Text );
                    break;
                default:
                    throw new Exception( "LaunchMode not set" );
            }
            if( RuntimeInfo.IsWindows ) {
                param += String.Format( " LAUNCHER_PATH=\"{0}\"", Paths.LauncherPath );
            }

            // hide the form, to avoid stealing focus from CM window
            Hide();
            Refresh();

            // launch CM
            try {
                Process.Start( binaryFileName, param );
                Application.Exit();
            } catch( Exception ex ) {
                Log( "StartChargedMiners ERROR: " + ex );
                State = FormState.UnrecoverableError;
                lStatus.Text = "Error launching Charged-Miners.";
                lStatus2.Text = ex.GetType().Name + Environment.NewLine + ex.Message;
            }
        }


        static readonly object LogLock = new object();


        public static void Log( string message ) {
            lock( LogLock ) {
                string fullMsg = String.Format( "* {0:HH':'mm':'ss'.'ff} {1}{2}",
                                                DateTime.Now, message, Environment.NewLine );
#if DEBUG
                Debugger.Log( 0, "Debug", fullMsg );
#endif
                File.AppendAllText( Paths.LauncherLogPath, fullMsg );
            }
        }

        private void xFailSafe_CheckedChanged( object sender, EventArgs e ) {
            SettingsFile sf = new SettingsFile();
            if( File.Exists( Paths.GameSettingsPath ) ) {
                sf.Load( Paths.GameSettingsPath );
            }
            bool failSafeEnabled = sf.GetBool( "mc.failsafe", false );
            if( failSafeEnabled != xFailSafe.Checked ) {
                sf.Set( "mc.failsafe", xFailSafe.Checked );
                sf.Save( Paths.GameSettingsPath );
            }
            lToolStatus.Text = "Fail-safe mode " + ( xFailSafe.Checked ? "enabled" : "disabled" ) + ".";
        }
    }
}
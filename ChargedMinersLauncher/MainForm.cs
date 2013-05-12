// Part of ChargedMinersLauncher | Copyright (c) 2012-2013 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChargedMinersLauncher {
    public sealed partial class MainForm : Form {
        static MainForm instance;
        const string WindowTitle = "Charged-Miners Launcher 1.22";

        #region Startup / Shutdown

        public MainForm() {
            instance = this;

            // Start logging
            if( File.Exists( Paths.LauncherLogFile ) ) {
                File.Delete( Paths.LauncherLogFile + ".old" );
                File.Move( Paths.LauncherLogFile, Paths.LauncherLogFile + ".old" );
            }
            Log( "---- " + DateTime.Now.ToLongDateString() + " ----" );
            Log( WindowTitle );

            // Set up the GUI
            InitializeComponent();
            SetToolTips();
            lOptionsStatus.Text = "";
            lToolStatus.Text = "";
            Text = WindowTitle;

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
            if( File.Exists( Paths.LauncherSettingsFile ) ) {
                settings.Load( Paths.LauncherSettingsFile );
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
            
#if TEST_ENCRYPTION
            PasswordSecurity.EncryptionTest( 1000 );
#endif
            LoadAccounts();

            // trigger input validation
            SignInFieldChanged( cSignInUsername, EventArgs.Empty );
            tDirectUrl_TextChanged( tDirectUrl, EventArgs.Empty );

            State = FormState.AtMainForm;

            // Load "Resume" information from CM's settings file
            LoadResumeInfo();

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


        void LoadResumeInfo() {
            if( File.Exists( Paths.GameSettingsFile ) ) {
                SettingsFile gameSettings = new SettingsFile();
                gameSettings.Load( Paths.GameSettingsFile );
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
                        return;
                    }
                }
            }
            ResetResumeInfo();
        }


        void ResetResumeInfo() {
            bResume.Enabled = false;
            tResumeUri.Text = "(Not Available)";
            tResumeUsername.Text = "?";
            tResumeServerIP.Text = "?";
            tResumeServerName.Text = "?";
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
                         "Uploads your Charged-Miners and launcher log files, which contain debugging information, for easy sharing.",
                     ToolTipMultiAccount =
                         "Whether Charged-Miners launcher should remember all used accounts, or just the most-recent one.",
                     ToolTipForgetAccount = "Forget any stored information about the currently-selected account.",
                     ToolTipFailSafe = "Start Charged-Miners in compatible, fail-safe mode. Use this if you're crashing while loading.";


        void SetToolTips() {
            toolTip = new ToolTip();
            toolTip.SetToolTip( cSignInUsername, ToolTipSignInUsername );
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
            toolTip.SetToolTip( xMultiUser, ToolTipMultiAccount );
            toolTip.SetToolTip( xFailSafe, ToolTipFailSafe );

            toolTip.SetToolTip( bResetSettings, ToolTipResetSettings );
            toolTip.SetToolTip( bDeleteData, ToolTipDeleteData );
            toolTip.SetToolTip( bOpenDataDir, ToolTipOpenDataDir );
            toolTip.SetToolTip( bUploadLog, ToolTipUploadLog );
            toolTip.SetToolTip( bForgetActiveAccount, ToolTipForgetAccount );
        }


        // log close reason
        static void OnFormClosed( object sender, FormClosedEventArgs e ) {
            Log( "Closed: " + e.CloseReason );
        }

        #endregion


        #region UI Event Hooks

        static readonly Color InvalidFieldColor = Color.Yellow,
                              ValidFieldColor = SystemColors.Window,
                              StatusWarningColor = Color.Red,
                              StatusNotifyColor = SystemColors.ControlDarkDark;

        // ==== Sign-In tab ====
        void SignInFieldChanged( object sender, EventArgs e ) {
            lSignInStatus.Text = "";

            bool canSignIn = false;
            // check the username field
            if( UsernameRegex.IsMatch( cSignInUsername.Text ) || EmailRegex.IsMatch( cSignInUsername.Text ) ) {
                cSignInUsername.BackColor = ValidFieldColor;
                canSignIn = true;
            } else {
                cSignInUsername.BackColor = InvalidFieldColor;
                lSignInStatus.Text = "Invalid username/email.";
            }

            // check the password field
            if( tSignInPassword.Text.Length == 0 ) {
                canSignIn = false;
                tSignInPassword.BackColor = InvalidFieldColor;
                if( sender == tSignInPassword || lSignInStatus.Text.Length == 0 ) {
                    lSignInStatus.Text = "Password is required.";
                }
            } else {
                tSignInPassword.BackColor = ValidFieldColor;
            }

            // check the URL field
            Match match = PlayLinkDirect.Match( tSignInUrl.Text );
            if( match.Success ) {
                // "mc://" url
                if( match.Groups[7].Value.Equals( cSignInUsername.Text, StringComparison.OrdinalIgnoreCase ) ) {
                    tSignInUrl.BackColor = ValidFieldColor;
                    launchMode = LaunchMode.SignInWithUri;
                } else {
                    canSignIn = false;
                    tSignInUrl.BackColor = InvalidFieldColor;
                    if( sender == tSignInUrl || lSignInStatus.Text.Length == 0 ) {
                        lSignInStatus.Text = "Given sign-in username does not match username in direct-connect URL.";
                    }
                }

            } else if( PlayLinkHash.IsMatch( tSignInUrl.Text ) || PlayLinkIPPort.IsMatch( tSignInUrl.Text ) ) {
                // minecraft.net play link
                launchMode = LaunchMode.SignInWithUri;
                tSignInUrl.BackColor = ValidFieldColor;

            } else if( tSignInUrl.Text.Length == 0 ) {
                // no URL given
                launchMode = LaunchMode.SignIn;
                tSignInUrl.BackColor = ValidFieldColor;

            } else {
                // unrecognized URL given
                tSignInUrl.BackColor = InvalidFieldColor;
                if( sender == tSignInUrl || lSignInStatus.Text.Length == 0 ) {
                    lSignInStatus.Text = "Unrecognized URL";
                }
                canSignIn = false;
            }

            bSignIn.Enabled = canSignIn;
            // let user know whether username/password will be remembered
            if( canSignIn && lSignInStatus.Text == "" ) {
                if( xRememberUsername.Checked ) {
                    lSignInStatus.ForeColor = StatusNotifyColor;
                    if( xRememberPassword.Checked ) {
                        lSignInStatus.Text = "Username and password will be remembered.";
                    } else {
                        lSignInStatus.Text = "Username will be remembered.";
                    }
                } else {
                    lSignInStatus.ForeColor = StatusWarningColor;
                }
            } else {
                lSignInStatus.ForeColor = StatusWarningColor;
            }
        }


        void bSignIn_Click( object sender, EventArgs e ) {
            Log( "[SignIn]" );

            string minecraftUsername;
            if( xRememberUsername.Checked && activeAccount != null &&
                cSignInUsername.Text == activeAccount.SignInUsername ) {
                minecraftUsername = activeAccount.PlayerName;
            } else {
                minecraftUsername = cSignInUsername.Text;
            }
            signInSession = new MinecraftNetSession( cSignInUsername.Text, minecraftUsername, tSignInPassword.Text );

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
                tDirectUrl.BackColor = ValidFieldColor;
                launchMode = LaunchMode.Direct;
                lDirectStatus.Text = "";
                bDirectConnect.Enabled = true;

            } else {
                // Unacceptable or missing URL
                tDirectServerIP.Text = "?";
                tDirectUsername.Text = "?";
                tDirectUrl.BackColor = InvalidFieldColor;
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
            if( !settingsLoaded )
                return;
            Log( "SaveLauncherSettings" );

            // confirm erasing accounts
            if( sender == xRememberUsername && !xRememberUsername.Checked &&
                !ConfirmDialog.Show( "Forget all usernames?",
                                     "When you uncheck \"remember usernames\", all currently-stored account " + 
                                     "information will be forgotten. Continue?" ) ) {
                xRememberUsername.Checked = true;
                lOptionsStatus.Text = "";
                return;
            }

            // confirm forgetting accounts
            if( sender == xMultiUser && !xMultiUser.Checked && accounts.Count > 1 &&
                !ConfirmDialog.Show( "Forget all other accounts?",
                                     "When you uncheck \"multiple accounts\", all account information other than " +
                                     "the currently-selected user will be forgotten. Continue?" ) ) {
                xMultiUser.Checked = true;
                lOptionsStatus.Text = "";
                return;
            }

            SettingsFile settings = new SettingsFile();
            settings.Set( "rememberUsername", xRememberUsername.Checked );
            settings.Set( "multiUser", xMultiUser.Checked );
            settings.Set( "rememberPassword", xRememberPassword.Checked );
            settings.Set( "rememberServer", xRememberServer.Checked );
            settings.Set( "gameUpdateMode", (GameUpdateMode)cGameUpdates.SelectedIndex );
            settings.Set( "startingTab", (StartingTab)cStartingTab.SelectedIndex );
            settings.Save( Paths.LauncherSettingsFile );

            if( sender == xRememberUsername ) {
                if( xRememberUsername.Checked ) {
                    xRememberPassword.Enabled = true;
                    lOptionsStatus.Text = "Usernames will now be remembered.";
                    xMultiUser.Enabled = true;
                } else {
                    xRememberPassword.Checked = false;
                    xRememberPassword.Enabled = false;
                    xMultiUser.Checked = false;
                    xMultiUser.Enabled = false;
                    accounts.RemoveAllAccounts();
                    LoadAccounts();
                    lOptionsStatus.Text = "Usernames will no longer be remembered.";
                }
                SignInFieldChanged( null, EventArgs.Empty );

            } else if( sender == xRememberPassword ) {
                if( xRememberPassword.Checked ) {
                    lOptionsStatus.Text = "Passwords will now be remembered.";
                } else {
                    lOptionsStatus.Text = "Passwords will no longer be remembered.";
                }
                SignInFieldChanged( null, EventArgs.Empty );

            } else if( sender == xRememberServer ) {
                if( xRememberServer.Checked ) {
                    lOptionsStatus.Text = "Last-joined server will now be remembered.";
                } else {
                    lOptionsStatus.Text = "Last-joined server will no longer be remembered.";
                }

            } else if( sender == xMultiUser ) {
                if( xMultiUser.Checked ) {
                    lOptionsStatus.Text = "All users will now be remembered.";
                } else {
                    lOptionsStatus.Text = "Only most-recent user will now be remembered.";
                }
                LoadAccounts();

            } else if( sender == cGameUpdates ) {
                lOptionsStatus.Text = "\"Game updates\" preference saved.";

            } else if( sender == cStartingTab ) {
                lOptionsStatus.Text = "\"Starting tab\" preference saved.";

            } else {
                lOptionsStatus.Text = "Preferences saved.";
            }
        }


        void xFailSafe_CheckedChanged( object sender, EventArgs e ) {
            SettingsFile sf = new SettingsFile();
            if( File.Exists( Paths.GameSettingsFile ) ) {
                sf.Load( Paths.GameSettingsFile );
            }
            bool failSafeEnabled = sf.GetBool( "mc.failsafe", false );
            if( failSafeEnabled != xFailSafe.Checked ) {
                sf.Set( "mc.failsafe", xFailSafe.Checked );
                sf.Save( Paths.GameSettingsFile );
            }
            lOptionsStatus.Text = "Fail-safe mode " + (xFailSafe.Checked ? "enabled" : "disabled") + ".";
        }


        // ==== Tools tab ====
        void bResetSettings_Click( object sender, EventArgs e ) {
            if( !ConfirmDialog.Show( "Reset settings?",
                                    "About to reset launcher and game settings to defaults. Continue?" ) ) {
                return;
            }
            Log( "[ResetSettings]" );
            File.Delete( Paths.LauncherSettingsFile );
            File.Delete( Paths.GameSettingsFile );
            settingsLoaded = false;
            LoadLauncherSettings();
            lToolStatus.Text = "Launcher and game settings were reset to defaults.";
        }


        void bDeleteData_Click( object sender, EventArgs e ) {
            if( !ConfirmDialog.Show( "Delete all data?",
                                    "About to reset all settings, remembered usernames/passwords, logs, etc. Continue?" ) ) {
                return;
            }
            Log( "[DeleteData]" );
            File.Delete( Paths.LauncherSettingsFile );
            File.Delete( Paths.GameSettingsFile );
            File.Delete( Paths.LauncherLogFile );
            File.Delete( Paths.GameLogFile );
            File.Delete( Path.Combine( Paths.DataDirectory, Paths.CookieContainerFile ) );
            File.Delete( Path.Combine( Paths.DataDirectory, Paths.LegacyPasswordSaveFile ) );
            File.Delete( Path.Combine( Paths.DataDirectory, Paths.AccountListFile ) );
            accounts.RemoveAllAccounts();
            settingsLoaded = false;
            LoadLauncherSettings();
            LoadAccounts();
            cSignInUsername.Text = "";
            tSignInPassword.Text = "";
            tSignInUrl.Text = "";
            tDirectUrl.Text = "";
            ResetResumeInfo();
            lToolStatus.Text = "All accounts, settings, logs, and saved data were deleted.";
        }


        void bOpenDataDir_Click( object sender, EventArgs e ) {
            Log( "[OpenDataDir]" );
            Process.Start( "file://" + Paths.DataDirectory );
        }


        void bUploadLog_Click( object sender, EventArgs e ) {
            Log( "[UploadLog]" );
            JsonObject files = new JsonObject();
            if( File.Exists( Paths.GameLogFile ) ) {
                string content = File.ReadAllText( Paths.GameLogFile );
                files.Add( "log.txt",
                           new JsonObject {
                               {
                                   "content", content
                               }
                           } );
            }
            if( File.Exists( Paths.LauncherLogFile ) ) {
                string content = File.ReadAllText( Paths.LauncherLogFile );
                files.Add( "launcher.log",
                           new JsonObject {
                               {
                                   "content", content
                               }
                           } );
            }
            if( File.Exists( Paths.LauncherLogFile + ".old" ) ) {
                string content = File.ReadAllText( Paths.LauncherLogFile + ".old" );
                files.Add( "launcher.log.old",
                           new JsonObject {
                               {
                                   "content", content
                               }
                           } );
            }
            if( files.Count == 0 ) {
                lToolStatus.Text = "No log files to submit";
                return;
            }

            JsonObject request = new JsonObject {
                {
                    "description", "Charged-Miners log upload"
                }, {
                    "public", false
                }, {
                    "files", files
                }
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


        void bForgetActiveAccount_Click( object sender, EventArgs e ) {
            if( !ConfirmDialog.Show( bForgetActiveAccount.Text + "?",
                                     "About to remove remembered username, password, and URL for account " +
                                     activeAccount.SignInUsername + ". Continue?" ) ) {
                return;
            }
            Log( "[ForgetAccount]" );
            accounts.RemoveAccount( activeAccount );
            lToolStatus.Text = "Removed information for selected account.";
            cSignInUsername.Text = "";
            tSignInPassword.Text = "";
            tSignInUrl.Text = "";
            bForgetActiveAccount.Enabled = false;
            bForgetActiveAccount.Text = "Forget account: (no account selected)";
            LoadAccounts();
        }

        #endregion


        #region Update Check / Update Download

        readonly BackgroundWorker versionCheckWorker = new BackgroundWorker();
        string localHashString;
        static readonly Uri UpdateUri = new Uri( "http://cdn.charged-miners.com/releases/" );
        readonly WebClient binaryDownloader = new WebClient();
        VersionInfo latestVersion;
        Exception updaterException;
        static readonly TimeSpan UpdateTimeout = TimeSpan.FromSeconds( 5 );


        void CheckUpdates( object sender, DoWorkEventArgs e ) {
            try {
                Log( "CheckUpdates" );
                VersionsTxt versionList;

                // download and parse version.txt
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create( UpdateUri + "version.txt" );
                request.Timeout = (int)UpdateTimeout.TotalMilliseconds;
                request.ReadWriteTimeout = (int)UpdateTimeout.TotalMilliseconds;
                request.CachePolicy = new RequestCachePolicy( RequestCacheLevel.BypassCache );
                request.Method = "GET";
                request.UserAgent = WindowTitle;
                using( HttpWebResponse response = (HttpWebResponse)request.GetResponse() ) {
                    using( Stream responseStream = response.GetResponseStream() ) {
                        StreamReader reader = new StreamReader( responseStream );
                        string updaterResponse = reader.ReadToEnd();
                        versionList = new VersionsTxt( updaterResponse.Split( '\n' ) );
                    }
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
                if( Paths.AlternativeBinary == null )
                    return;

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

            if( loginCompleted ) {
                OnSignInAndUpdateCheckCompleted();
            }
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
            if( e.Cancelled )
                return;
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


        #region Sign-In and Accounts

        MinecraftNetSession signInSession;
        LaunchMode launchMode;
        SignInAccount activeAccount;
        readonly AccountManager accounts = new AccountManager();
        readonly BackgroundWorker signInWorker = new BackgroundWorker();

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@" +
                                    @"(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                                    RegexOptions.IgnoreCase ),
            PlayLinkHash = new Regex( @"^http://(www\.)?minecraft.net/classic/play/([0-9a-fA-F]{28,32})/?(\?override=(true|1))?$" ),
            PlayLinkDirect = new Regex( @"^mc://((localhost|(\d{1,3}\.){3}\d{1,3}|([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-]+))(:\d{1,5})?)/" +
                                        @"([a-zA-Z0-9_\.]{2,16})(/.*)$" ),
            PlayLinkIPPort = new Regex( @"^http://(www\.)?minecraft.net/classic/play\?ip=" + 
                                        @"(localhost|(\d{1,3}\.){3}\d{1,3}|([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-]+))&port=(\d{1,5})$" );


        void SignIn( object sender, DoWorkEventArgs e ) {
            signInSession.Login( xRememberUsername.Checked && xRememberPassword.Checked );
        }


        void OnSignInCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            if( e.Error != null ) {
                signInSession.Status = LoginResult.Error;
            }
            Log( "OnSignInCompleted " + signInSession.Status );
            lSignInStatus.ForeColor = StatusWarningColor;

            switch( signInSession.Status ) {
                case LoginResult.Success:
                    SaveAccounts();
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
                    cSignInUsername.Select();
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


        // Used by worker threads to update "Signing In..." status in a thread-safe manner.
        internal static void SetSignInStatus( string text ) {
            if( instance.lStatus2.InvokeRequired ) {
                instance.lStatus2.BeginInvoke( (MethodInvoker)delegate { instance.lStatus2.Text = text; } );
            } else {
                instance.lStatus2.Text = text;
            }
        }


        void LoadAccounts() {
            try {
                // load stored account information
                accounts.LoadAccounts();
                if( File.Exists( Paths.LegacyPasswordSaveFile ) ) {
                    LoadLegacyPasswordSaveFile();
                }

                cSignInUsername.Items.Clear();
                if( xMultiUser.Checked ) {
                    // Multiple accounts! Add them all to the menu, recently-used first.
                    SignInAccount[] accountsByDate = accounts.GetAccountsBySignInDate();
                    foreach( SignInAccount account in accountsByDate ) {
                        cSignInUsername.Items.Add( account.SignInUsername );
                    }
                    if( accountsByDate.Length > 0 ) {
                        SelectActiveAccount( accountsByDate[0] );
                    } else {
                        SelectActiveAccount( null );
                    }

                } else {
                    // Single account. Add most-recently-used one to the menu.
                    SelectActiveAccount( accounts.GetMostRecentlyUsedAccount() );
                    if( activeAccount != null ) {
                        cSignInUsername.Items.Add( activeAccount.SignInUsername );
                    }
                }

            } catch( Exception ex ) {
                Log( "LoadAccounts: " + ex );
                lSignInStatus.Text = "Could not load saved login information.";
            }
        }


        // Load account info from the old password file (saved-login.dat). Delete it after done.
        void LoadLegacyPasswordSaveFile() {
            if( xRememberUsername.Checked ) {
                string[] loginData = File.ReadAllLines( Paths.LegacyPasswordSaveFile );

                SignInAccount oldAccount = new SignInAccount {
                    SignInUsername = loginData[0]
                };
                oldAccount.PlayerName = ( loginData.Length > 2 ? loginData[2] : oldAccount.SignInUsername );

                if( xRememberPassword.Checked ) {
                    oldAccount.Password = loginData[1];
                } else {
                    oldAccount.Password = "";
                }

                if( xRememberServer.Checked ) {
                    oldAccount.LastUrl = ( loginData.Length > 3 ? loginData[3] : "" );
                } else {
                    oldAccount.LastUrl = "";
                }

                oldAccount.SignInDate = DateTime.UtcNow;
                if( !accounts.HasAccount( oldAccount.SignInUsername ) ) {
                    accounts.AddAccount( oldAccount );
                }
                accounts.SaveAllAccounts();
            }
            File.Delete( Paths.LegacyPasswordSaveFile );
        }


        // Set given account as the active one.
        void SelectActiveAccount( SignInAccount account ) {
            activeAccount = account;
            if( account == null ) {
                bForgetActiveAccount.Enabled = false;
                bForgetActiveAccount.Text = "Forget account: (no account selected)";
            } else {
                bForgetActiveAccount.Enabled = true;
                bForgetActiveAccount.Text = "Forget account: " + activeAccount.SignInUsername;
                cSignInUsername.Text = account.SignInUsername;
                if( xRememberPassword.Checked ) {
                    tSignInPassword.Text = account.Password;
                } else {
                    tSignInPassword.Text = "";
                }
                if( xRememberServer.Checked && account.LastUrl.Length > 0 ) {
                    tSignInUrl.Text = account.LastUrl;
                }
            }
        }


        void SaveAccounts() {
            if( activeAccount == null ) {
                // If currenty-entered account information is not on record, add it to AccountManager
                activeAccount = new SignInAccount {
                    SignInUsername = cSignInUsername.Text,
                    Password = tSignInPassword.Text,
                    PlayerName = signInSession.MinercraftUsername,
                    LastUrl = tSignInUrl.Text
                };
                accounts.AddAccount( activeAccount );
            }

            activeAccount.SignInDate = DateTime.UtcNow;
            if( !xMultiUser.Checked ) {
                // If no multi-user, clear all accounts except active one
                accounts.RemoveAllAccounts();
                accounts.AddAccount( activeAccount );
            }
            accounts.SaveAllAccounts();
        }


        void cSignInUsername_TextChanged( object sender, EventArgs e ) {
            SignInFieldChanged( sender, e );
            string givenUsername = cSignInUsername.Text;
            SignInAccount acct = accounts.FindAccount( givenUsername );
            if( acct != null ) {
                // Recognized account! Load info from it.
                SelectActiveAccount( acct );
            } else {
                // Unrecognized account
                if( activeAccount != null ) {
                    // Reset password when going from known to unknown account
                    tSignInPassword.Text = "";
                }
                activeAccount = null;
                bForgetActiveAccount.Enabled = false;
                bForgetActiveAccount.Text = "Forget account: (no account selected)";
            }
        }


        // After selecting a username from the drop-down list,
        // go to next field that needs to be entered (password or URL)
        void cSignInUsername_SelectedIndexChanged( object sender, EventArgs e ) {
            if( tSignInPassword.TextLength == 0 ) {
                tSignInPassword.Focus();
            } else {
                tSignInUrl.Focus();
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
            get {
                return state;
            }
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
                        lStatus.Text = String.Format( "Signing in as {0}...", signInSession.LoginUsername );
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
            if( !tabs.Visible ) {
                return;
            }
            if( tabs.SelectedTab == tabSignIn ) {
                AcceptButton = bSignIn;
                if( cSignInUsername.Text.Length == 0 ) {
                    cSignInUsername.Focus();
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
            } else if( tabs.SelectedTab == tabOptions ) {
                AcceptButton = null;
                SettingsFile sf = new SettingsFile();
                if( File.Exists( Paths.GameSettingsFile ) ) {
                    sf.Load( Paths.GameSettingsFile );
                }
                xFailSafe.Checked = sf.GetBool( "mc.failsafe", false );
            } else {
                AcceptButton = null;
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
                param += String.Format( " LAUNCHER_PATH=\"{0}\"", Paths.LauncherBinaryFile );
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
                                                DateTime.Now,
                                                message,
                                                Environment.NewLine );
#if DEBUG
                Debugger.Log( 0, "Debug", fullMsg );
#endif
                File.AppendAllText( Paths.LauncherLogFile, fullMsg );
            }
        }
    }
}
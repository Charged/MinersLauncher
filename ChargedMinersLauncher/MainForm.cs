﻿// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
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
        public MainForm() {
            // Start logging
            if( File.Exists( Paths.LauncherLogPath ) ) {
                File.Delete( Paths.LauncherLogPath + ".old" );
                File.Move( Paths.LauncherLogPath, Paths.LauncherLogPath + ".old" );
            }
            Log( "---- " + DateTime.Now.ToLongDateString() + " ----" );

            // Set up the GUI
            InitializeComponent();
            SetToolTips();
            lOptionsSaved.Text = "";
            lToolStatus.Text = "";

            State = FormState.AtSignInForm;

            // hook up event handlers
            signInWorker.DoWork += SignIn;
            signInWorker.RunWorkerCompleted += OnSignInCompleted;
            signInWorker.WorkerSupportsCancellation = true;

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


        #region Startup

        bool settingsLoaded;


        void LoadLauncherSettings() {
            Log( "LoadLauncherSettings" );
            SettingsFile settings = new SettingsFile();
            if( File.Exists( Paths.LauncherSettingsPath ) ) {
                settings.Load( Paths.LauncherSettingsPath );
            }
            bool saveUsername = settings.GetBool( "rememberUsername", true );
            bool savePassword = settings.GetBool( "rememberPassword", false );
            bool saveUrl = settings.GetBool( "rememberServer", true );
            GameUpdateMode gameUpdateMode = settings.GetEnum( "gameUpdateMode", GameUpdateMode.Ask );
            StartingTab startingTab = settings.GetEnum( "startingTab", StartingTab.SignIn );
            xRememberUsername.Checked = saveUsername;
            xRememberPassword.Checked = savePassword;
            xRememberServer.Checked = saveUrl;
            cGameUpdates.SelectedIndex = (int)gameUpdateMode;
            cStartingTab.SelectedIndex = (int)startingTab;
            settingsLoaded = true;
        }


        void OnShown( object sender, EventArgs e ) {
            if( !Paths.IsPlatformSupported ) {
                State = FormState.PlatformNotSupportedError;
                return;
            }

            LoadLoginInfo();

            // trigger input validation
            SignInFieldChanged( tSignInUsername, EventArgs.Empty );
            tDirectUrl_TextChanged( tDirectUrl, EventArgs.Empty );

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
                        bResume.Enabled = false;
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

        const string ToolTipUsername = "Your minecraft.net username or email",
                     ToolTipPassword = "Your minecraft.net password",
                     ToolTipRemember = "Save your username and password for next time. Note that password is stored in plain text.",
                     ToolTipUri = "Server's Minecraft.net URL or a DirectConnect (mc://) link. Optional.",
                     ToolTipResume = "Try to reuse the last-used credentials, to connect to the most-recently-joined server.";


        void SetToolTips() {
            toolTip = new ToolTip();
            toolTip.SetToolTip( tSignInUsername, ToolTipUsername );
            toolTip.SetToolTip( lUsername, ToolTipUsername );
            toolTip.SetToolTip( tSignInPassword, ToolTipPassword );
            toolTip.SetToolTip( lPassword, ToolTipPassword );
            toolTip.SetToolTip( xRememberUsername, ToolTipRemember );
            toolTip.SetToolTip( tDirectUrl, ToolTipUri );
            toolTip.SetToolTip( lDirectUrl, ToolTipUri );
            toolTip.SetToolTip( bDirectConnect, ToolTipResume );
        }


        // log close reason
        void OnFormClosed( object sender, FormClosedEventArgs e ) {
            Log( "Closed: " + e.CloseReason );
        }

        #endregion


        #region UI Event Hooks

        // Sign-In tab
        void SignInFieldChanged( object sender, EventArgs e ) {
            lSignInStatus.Text = "";

            bool canSignIn = false;
            // check the username field
            if( UsernameRegex.IsMatch( tSignInUsername.Text ) || EmailRegex.IsMatch( tSignInUsername.Text ) ) {
                tSignInUsername.BackColor = SystemColors.Window;
                canSignIn = true;
            } else {
                tSignInUsername.BackColor = Color.Yellow;
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
                if( match.Groups[7].Value.Equals( tSignInUsername.Text, StringComparison.OrdinalIgnoreCase ) ) {
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
            string minecraftUsername;
            if( tSignInUsername.Text == storedLoginUsername ) {
                minecraftUsername = storedMinecraftUsername;
            } else {
                minecraftUsername = tSignInUsername.Text;
            }
            signInSession = new MinecraftNetSession( tSignInUsername.Text, minecraftUsername, tSignInPassword.Text );

            State = FormState.SigningIn;
            signInWorker.RunWorkerAsync();
        }


        // Resume tab
        void bResume_Click( object sender, EventArgs e ) {
            launchMode = LaunchMode.Resume;
            loginCompleted = true;
            if( updateCheckCompleted ) {
                OnSignInAndUpdateCheckCompleted();
            } else {
                State = FormState.WaitingForUpdater;
            }
        }


        // Direct tab
        void tDirectUrl_TextChanged( object sender, EventArgs e ) {
            // check the URL field
            Match match = PlayLinkDirect.Match( tDirectUrl.Text );
            if( match.Success ) {
                // "mc://" url
                tDirectServerIP.Text = match.Groups[1].Value;
                tDirectUsername.Text = match.Groups[7].Value;
                tDirectUrl.BackColor = SystemColors.Window;
                launchMode = LaunchMode.Direct;
                lDirectStatus.Text = "";
                bResume.Enabled = true;

            } else {
                tDirectServerIP.Text = "?";
                tDirectUsername.Text = "?";
                tDirectUrl.BackColor = Color.Yellow;
                bResume.Enabled = false;
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


        // Options tab
        void SaveLauncherSettings( object sender, EventArgs e ) {
            if( !settingsLoaded ) return;
            Log( "SaveLauncherSettings" );
            SettingsFile settings = new SettingsFile();
            settings.Set( "rememberUsername", xRememberUsername.Checked );
            settings.Set( "rememberPassword", xRememberPassword.Checked );
            settings.Set( "rememberServer", xRememberServer.Checked );
            settings.Set( "gameUpdateMode", (GameUpdateMode)cGameUpdates.SelectedIndex );
            settings.Set( "startingTab", (StartingTab)cStartingTab.SelectedIndex );
            settings.Save( Paths.LauncherSettingsPath );
            lOptionsSaved.Text = "Changes saved.";
        }


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

                // if not, fail
                if( latestVersion == null ) return;

                // check if local alt binary exists
                if( File.Exists( Paths.AlternativeBinary ) ) {
                    localHashString = ComputeLocalHash( Paths.AlternativeBinary );
                } // else download alt binary
            } catch( Exception ex ) {
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

        bool directConnect,
             clickedGo;

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
                new Regex(
                    @"^mc://((localhost|(\d{1,3}\.){3}\d{1,3}|([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-]+))(:\d{1,5})?)/([a-zA-Z0-9_\.]{2,16})/(.*)$" ),
            PlayLinkIPPort =
                new Regex(
                    @"^http://(www\.)?minecraft.net/classic/play/?\?ip=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})&port=(\d{1,5})$" );


        void SignIn( object sender, DoWorkEventArgs e ) {
            try {
                signInSession.Login( xRememberUsername.Checked );
            } catch( WebException ex ) {
                signInSession.LoginException = ex;
                signInSession.Status = LoginResult.Error;
            }
        }


        void OnSignInCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            if( e.Cancelled ) {
                Log( "OnSignInCompleted Canceled" );
                State = FormState.AtSignInForm;
                return;
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
                    lSignInStatus.Text = "Migrated account. Use your email to sign in.";
                    SelectedPanel = tabs;
                    break;

                case LoginResult.WrongUsernameOrPass:
                    lSignInStatus.Text = "Wrong username or password.";
                    SelectedPanel = tabs;
                    break;

                case LoginResult.UnrecognizedResponse:
                    lSignInStatus.Text = "Could not understand minecraft.net response.";
                    SelectedPanel = tabs;
                    break;

                case LoginResult.Error:
                    Exception ex = signInSession.LoginException;
                    if( ex != null ) {
                        Log( "LoginException: " + ex );
                        lSignInStatus.Text = "Error: " + ex.Message;
                    } else {
                        lSignInStatus.Text = "An unknown error occurred.";
                    }
                    SelectedPanel = tabs;
                    break;
            }
        }


        void LoadLoginInfo() {
            try {
                if( File.Exists( Paths.PasswordSaveFile ) ) {
                    string[] loginData = File.ReadAllLines( Paths.PasswordSaveFile );
                    storedLoginUsername = loginData[0];
                    tSignInUsername.Text = storedLoginUsername;
                    tSignInPassword.Text = loginData[1];
                    storedMinecraftUsername = loginData.Length > 2 ? loginData[2] : storedLoginUsername;
                    xRememberUsername.Checked = true;
                }
            } catch( Exception ) {
                lSignInStatus.Text = "Could not load saved login information.";
            }
        }


        void SaveLoginInfo() {
            if( xRememberUsername.Checked ) {
                File.WriteAllLines( Paths.PasswordSaveFile, new[] {
                    signInSession.LoginUsername,
                    signInSession.Password,
                    signInSession.MinercraftUsername
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
                // no CM version found for this platform; notify, then terminate
                State = FormState.PlatformNotSupportedError;

            } else if( !File.Exists( latestVersion.Name ) ) {
                // no local CM binaries: download them
                if( downloadComplete ) {
                    ApplyUpdate();
                } else {
                    State = FormState.DownloadingBinary;
                }

            } else if( !latestVersion.Md5.Equals( localHashString, StringComparison.OrdinalIgnoreCase ) ) {
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
                    case FormState.AtSignInForm:
                        AcceptButton = bSignIn;
                        CancelButton = null;
                        lStatus.Text = "";
                        lStatus2.Text = "";
                        SelectedPanel = tabs;
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

                    case FormState.PlatformNotSupportedError:
                        AcceptButton = bCancel;
                        CancelButton = null;
                        lStatus.Text = "Failed to initialize";
                        lStatus2.Text = "Charged-Miners is not available for this platform.";
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
            get { return selectedPanel; }
            set {
                if( value == tabs ) {
                    panelStatus.Visible = false;
                    panelUpdatePrompt.Visible = false;
                    tabs.Visible = true;
                } else if( value == panelStatus ) {
                    panelUpdatePrompt.Visible = false;
                    tabs.Visible = false;
                    panelStatus.Visible = true;
                } else {
                    tabs.Visible = false;
                    panelStatus.Visible = false;
                    panelUpdatePrompt.Visible = true;
                }
                selectedPanel = value;
            }
        }

        Control selectedPanel;


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
                    signInWorker.CancelAsync();
                    lStatus2.Text = "Canceling...";
                    break;
                case FormState.PlatformNotSupportedError:
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

            // if we are on unix, set +x on Charge binaries
            if( RuntimeInfo.IsUnix ) {
                Process chmod = new Process {
                    StartInfo = {
                        FileName = "chmod",
                        Arguments = "a+x " + latestVersion.Name,
                        UseShellExecute = true
                    }
                };
                chmod.Start();
                chmod.WaitForExit();
            }

            // hide the form, to avoid stealing focus from CM window
            Hide();
            
            string param;
            switch( launchMode ) {
                case LaunchMode.Direct:
                    param = tDirectUrl.Text;
                    break;
                case LaunchMode.Resume:
                    param = tResumeUri.Text;
                    break;
                case LaunchMode.SignIn:
                    param = "PLAY_SESSION=" + signInSession.PlaySessionCookie;
                    break;
                case LaunchMode.SignInWithUri:
                    param = String.Format( "PLAY_SESSION={0} {1}",
                                           signInSession.PlaySessionCookie,
                                           tSignInUrl.Text );
                    break;
                default:
                    throw new Exception( "LaunchMode not set" );
            }

            if( RuntimeInfo.IsWindows ) {
                param += String.Format( " LAUNCHER_PATH=\"{0}\"", Paths.LauncherPath );
            }

            Process.Start( latestVersion.Name, param );
            Application.Exit();
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
    }
}
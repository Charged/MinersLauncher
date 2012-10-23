// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
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

        Panel SelectedPanel {
            get { return selectedPanel; }
            set {
                if( value == panelSignIn ) {
                    panelStatus.Visible = false;
                    panelUpdatePrompt.Visible = false;
                    panelSignIn.Visible = true;
                } else if( value == panelStatus ) {
                    panelUpdatePrompt.Visible = false;
                    panelSignIn.Visible = false;
                    panelStatus.Visible = true;
                } else {
                    panelSignIn.Visible = false;
                    panelStatus.Visible = false;
                    panelUpdatePrompt.Visible = true;
                }
                selectedPanel = value;
            }
        }

        Panel selectedPanel;


        public MainForm() {
            Log( "---- " + DateTime.Now.ToLongDateString() + " ----" );
            InitializeComponent();

            SetToolTips();

            lSignInStatus.Text = "";
            State = FormState.AtSignInForm;

            signInWorker.DoWork += SignIn;
            signInWorker.RunWorkerCompleted += OnSignInCompleted;
            signInWorker.WorkerSupportsCancellation = true;

            versionCheckWorker.DoWork += CheckUpdates;
            versionCheckWorker.RunWorkerCompleted += OnUpdateCheckCompleted;

            binaryDownloader.DownloadProgressChanged += OnDownloadProgress;
            binaryDownloader.DownloadFileCompleted += OnDownloadCompleted;
        }


        #region Check Updates / Download

        readonly BackgroundWorker versionCheckWorker = new BackgroundWorker();
        string localHashString;
        static readonly Uri UpdateUri = new Uri( "http://cloud.github.com/downloads/Charged/Miners/" );
        readonly WebClient binaryDownloader = new WebClient();
        VersionInfo latestVersion;


        void CheckUpdates( object sender, DoWorkEventArgs e ) {
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
                    @"^mc://((\d{1,3}\.){3}\d{1,3}|([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-]+))(:\d{1,5})?/([a-zA-Z0-9_\.]{2,16})/.*$" ),
            PlayLinkIPPort =
                new Regex(
                    @"^http://(www\.)?minecraft.net/classic/play/?\?ip=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})&port=(\d{1,5})$" );


        void OnUsernameOrPasswordChanged( object sender, EventArgs e ) {
            bool canSignIn = false;
            if( UsernameRegex.IsMatch( tUsername.Text ) || EmailRegex.IsMatch( tUsername.Text ) ) {
                tUsername.BackColor = SystemColors.Window;
                canSignIn = true;
            } else {
                tUsername.BackColor = Color.Yellow;
            }
            canSignIn |= ( tPassword.Text.Length > 0 );
            bSignIn.Enabled = canSignIn;
            tURL_TextChanged( sender, e );
        }


        void tURL_TextChanged( object sender, EventArgs e ) {
            if( PlayLinkDirect.IsMatch( tUri.Text ) ) {
                // "mc://" url
                tUri.BackColor = SystemColors.Window;
                bGo.Enabled = true;
                directConnect = true;
            } else {
                directConnect = false;
                if( PlayLinkHash.IsMatch( tUri.Text ) || PlayLinkIPPort.IsMatch( tUri.Text ) ) {
                    // minecraft.net play link
                    tUri.BackColor = SystemColors.Window;
                    bGo.Enabled = bSignIn.Enabled;
                } else if( tUri.Text.Length == 0 ) {
                    // no URL given
                    tUri.BackColor = SystemColors.Window;
                    bGo.Enabled = bSignIn.Enabled;
                } else {
                    // unrecognized URL given
                    tUri.BackColor = Color.Yellow;
                    bGo.Enabled = false;
                }
            }
        }


        void bSignIn_Click( object sender, EventArgs e ) {
            directConnect = false;
            string minecraftUsername;
            if( tUsername.Text == storedLoginUsername ) {
                minecraftUsername = storedMinecraftUsername;
            } else {
                minecraftUsername = tUsername.Text;
            }
            MinecraftNetSession.Instance = new MinecraftNetSession( tUsername.Text, minecraftUsername, tPassword.Text );

            State = FormState.SigningIn;
            signInWorker.RunWorkerAsync();
        }


        void bGo_Click( object sender, EventArgs e ) {
            clickedGo = true;
            if( directConnect ) {
                loginCompleted = true;
                if( updateCheckCompleted ) {
                    OnSignInAndUpdateCheckCompleted();
                } else {
                    State = FormState.WaitingForUpdater;
                }
            } else {
                bSignIn_Click( sender, e );
            }
        }


        void SignIn( object sender, DoWorkEventArgs e ) {
            try {
                MinecraftNetSession.Instance.Login( xRemember.Checked );
            } catch( WebException ex ) {
                MinecraftNetSession.Instance.LoginException = ex;
                MinecraftNetSession.Instance.Status = LoginResult.Error;
            }
        }


        void OnSignInCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            Log( "OnSignInCompleted" );
            if( e.Cancelled ) {
                State = FormState.AtSignInForm;
                return;
            }
            switch( MinecraftNetSession.Instance.Status ) {
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
                    SelectedPanel = panelSignIn;
                    break;

                case LoginResult.WrongUsernameOrPass:
                    lSignInStatus.Text = "Wrong username or password.";
                    SelectedPanel = panelSignIn;
                    break;

                case LoginResult.UnrecognizedResponse:
                    lSignInStatus.Text = "Could not understand minecraft.net response.";
                    SelectedPanel = panelSignIn;
                    break;

                case LoginResult.Error:
                    Exception ex = MinecraftNetSession.Instance.LoginException;
                    if( ex != null ) {
                        lSignInStatus.Text = "Error: " + ex.Message;
                    } else {
                        lSignInStatus.Text = "An unknown error occurred.";
                    }
                    SelectedPanel = panelSignIn;
                    break;
            }
        }


        void LoadLoginInfo() {
            try {
                if( File.Exists( Paths.PasswordSaveFile ) ) {
                    string[] loginData = File.ReadAllLines( Paths.PasswordSaveFile );
                    storedLoginUsername = loginData[0];
                    tUsername.Text = storedLoginUsername;
                    tPassword.Text = loginData[1];
                    storedMinecraftUsername = loginData.Length > 2 ? loginData[2] : storedLoginUsername;
                    xRemember.Checked = true;
                }
            } catch( Exception ) {
                lSignInStatus.Text = "Could not load saved login information.";
            }
        }


        void SaveLoginInfo() {
            if( xRemember.Checked ) {
                File.WriteAllLines( Paths.PasswordSaveFile, new[] {
                    MinecraftNetSession.Instance.LoginUsername,
                    MinecraftNetSession.Instance.Password,
                    MinecraftNetSession.Instance.MinercraftUsername
                } );
            } else {
                if( File.Exists( Paths.PasswordSaveFile ) ) {
                    File.Delete( Paths.PasswordSaveFile );
                }
            }
        }

        #endregion


        #region State Control

        volatile bool loginCompleted, updateCheckCompleted, downloadComplete;

        static readonly Regex ChargedMinersLastServer = new Regex( @"^mc\.lastMcUrl:(mc.*)$" );


        protected override void OnShown( EventArgs e ) {
            if( Paths.IsPlatformSupported ) {
                LoadLoginInfo();

                // fill in the Uri field with CM's last-joined server
                if( File.Exists( Paths.SettingsPath ) ) {
                    string[] config = File.ReadAllLines( Paths.SettingsPath );
                    foreach( string line in config ) {
                        Match configMatch = ChargedMinersLastServer.Match( line );
                        if( configMatch.Success ) {
                            tUri.Text = configMatch.Groups[1].Value;
                            break;
                        }
                    }
                }

                versionCheckWorker.RunWorkerAsync();
            } else {
                State = FormState.PlatformNotSupportedError;
            }
            base.OnShown( e );
        }


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
                // local CM binaries are outdated; prompt to update
                State = FormState.PromptingToUpdate;

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
                        SelectedPanel = panelSignIn;
                        break;

                    case FormState.SigningIn:
                        AcceptButton = null;
                        CancelButton = bCancel;
                        lSignInStatus.Text = "";
                        lStatus.Text = String.Format( "Signing in as {0}...",
                                                      MinecraftNetSession.Instance.LoginUsername );
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


        void bUpdateYes_Click( object sender, EventArgs e ) {
            if( downloadComplete ) {
                ApplyUpdate();
            } else {
                State = FormState.DownloadingBinary;
            }
        }


        void bUpdateNo_Click( object sender, EventArgs e ) {
            binaryDownloader.CancelAsync();
            StartChargedMiners();
        }


        void bCancel_Click( object sender, EventArgs e ) {
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


        #region ToolTips

        ToolTip toolTip;

        const string ToolTipUsername = "Your minecraft.net username or email",
                     ToolTipPassword = "Your minecraft.net password",
                     ToolTipRemember =
                         "Save your username and password for next time. Note that password is stored in plain text.",
                     ToolTipUri = "Server's Minecraft.net URL or a DirectConnect (mc://) link. Optional.",
                     ToolTipResume =
                         "Try to reuse the last-used credentials, to connect to the most-recently-joined server.";


        void SetToolTips() {
            toolTip = new ToolTip();
            toolTip.SetToolTip( tUsername, ToolTipUsername );
            toolTip.SetToolTip( lUsername, ToolTipUsername );
            toolTip.SetToolTip( tPassword, ToolTipPassword );
            toolTip.SetToolTip( lPassword, ToolTipPassword );
            toolTip.SetToolTip( xRemember, ToolTipRemember );
            toolTip.SetToolTip( tUri, ToolTipUri );
            toolTip.SetToolTip( lUri, ToolTipUri );
            toolTip.SetToolTip( bGo, ToolTipResume );
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
            if( directConnect ) {
                // for direct-connect URIs (no session)
                param = tUri.Text;

            } else if( clickedGo ) {
                // for minecraft.net URIs
                param = String.Format( "PLAY_SESSION={0} {1}",
                                       MinecraftNetSession.Instance.PlaySessionCookie,
                                       tUri.Text );
            } else {
                // pass session only
                param = "PLAY_SESSION=" + MinecraftNetSession.Instance.PlaySessionCookie;
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

        private void MainForm_FormClosed( object sender, FormClosedEventArgs e ) {
            Log( "Closed: " + e.CloseReason );
        }
    }
}
// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

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
            InitializeComponent();

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
        static readonly Uri UpdateUri = new Uri( "http://cloud.github.com/downloads/Wallbraker/Charged-Miners/" );
        readonly WebClient binaryDownloader = new WebClient();
        VersionInfo latestVersion;

        void CheckUpdates( object sender, DoWorkEventArgs e ) {
            Log( "CheckUpdates" );
            // download and parse version.txt
            WebClient updateCheckDownloader = new WebClient();
            string versions = updateCheckDownloader.DownloadString( UpdateUri + "version.txt" );
            VersionsTxt versionList = new VersionsTxt( versions.Split( '\n' ) );

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
        string ComputeLocalHash( string fileName ) {
            MD5 hasher = MD5.Create();
            using( FileStream fs = File.OpenRead( fileName ) ) {
                StringBuilder sb = new StringBuilder( 32 );
                foreach( byte b in hasher.ComputeHash( fs ) ) {
                    sb.AppendFormat( "{0:x2}", b );
                }
                return sb.ToString();
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

        string storedLoginUsername,
               storedMinecraftUsername;
        readonly BackgroundWorker signInWorker = new BackgroundWorker();
        static readonly Regex UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
                              EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@" +
                                                      @"(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                                                      RegexOptions.IgnoreCase );


        void OnUsernameOrPasswordChanged( object sender, EventArgs e ) {
            if( UsernameRegex.IsMatch( tUsername.Text ) || EmailRegex.IsMatch( tUsername.Text ) ) {
                tUsername.BackColor = SystemColors.Window;
            } else {
                tUsername.BackColor = Color.Yellow;
            }
            bSignIn.Enabled = ( tPassword.Text.Length > 0 );
        }


        void bSignIn_Click( object sender, EventArgs e ) {
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

                case LoginResult.WrongUsernameOrPass:
                    lSignInStatus.Text = "Wrong username or password.";
                    SelectedPanel = panelSignIn;
                    break;

                case LoginResult.Error:
                    Exception ex = MinecraftNetSession.Instance.LoginException;
                    if( ex != null ) {
                        lSignInStatus.Text = "Error: " + ex.Message;
                    } else {
                        lSignInStatus.Text = "An unknown error occured.";
                    }
                    SelectedPanel = panelSignIn;
                    break;
            }
        }


        void LoadLoginInfo() {
            string passwordFileFullName = Paths.PasswordSaveFile;
            try {
                if( File.Exists( passwordFileFullName ) ) {
                    string[] loginData = File.ReadAllLines( passwordFileFullName );
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
            string passwordFileFullName = Paths.PasswordSaveFile;
            if( xRemember.Checked ) {
                File.WriteAllLines( passwordFileFullName, new[] {
                                                                    MinecraftNetSession.Instance.LoginUsername,
                                                                    MinecraftNetSession.Instance.Password,
                                                                    MinecraftNetSession.Instance.MinercraftUsername
                                                                } );
            } else {
                if( File.Exists( passwordFileFullName ) ) {
                    File.Delete( passwordFileFullName );
                }
            }
        }

        #endregion


        #region State Control

        bool loginCompleted, updateCheckCompleted, downloadComplete;


        protected override void OnShown( EventArgs e ) {
            if( Paths.Init() ) {
                LoadLoginInfo();
                versionCheckWorker.RunWorkerAsync();
            } else {
                State = FormState.PlatformNotSupportedError;
            }
            base.OnShown( e );
        }


        void OnSignInAndUpdateCheckCompleted() {
            Log( "OnSignInAndUpdateCheckCompleted" );
            if( latestVersion == null ) {
                State = FormState.PlatformNotSupportedError;
            } else if( !File.Exists( latestVersion.Name ) ) {
                if( downloadComplete ) {
                    ApplyUpdate();
                } else {
                    State = FormState.DownloadingBinary;
                }
            } else if( !latestVersion.Md5.Equals( localHashString, StringComparison.OrdinalIgnoreCase ) ) {
                State = FormState.PromptingToUpdate;
            } else {
                StartChargedMiners();
            }
        }


        FormState State {
            get { return state; }
            set {
                Log( "State = " + value );
                switch( value ) {
                    case FormState.AtSignInForm:
                        lStatus.Text = "";
                        lStatus2.Text = "";
                        SelectedPanel = panelSignIn;
                        break;

                    case FormState.SigningIn:
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
                        lSignInStatus.Text = "";
                        lStatus.Text = "Checking for updates...";
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Marquee;
                        bCancel.Visible = false;
                        break;

                    case FormState.PlatformNotSupportedError:
                        lStatus.Text = "Failed to initialize";
                        lStatus2.Text = "Charged-Miners is not available for this platform.";
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 100;
                        bCancel.Text = "OK";
                        bCancel.Visible = true;
                        break;

                    case FormState.PromptingToUpdate:
                        SelectedPanel = panelUpdatePrompt;
                        break;

                    case FormState.DownloadingBinary:
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


        void StartChargedMiners() {
            Hide();
            Log( "StartChargedMiners" );
            Process.Start( latestVersion.Name, "PLAY_SESSION=" + MinecraftNetSession.Instance.PlaySessionCookie );
            Application.Exit();
        }


        static void Log( string message ) {
#if DEBUG
            string fullMsg = String.Format( "* {0:HH':'mm':'ss'.'ff} {1} {2}",
                                            DateTime.Now, message, Environment.NewLine );
            Debugger.Log( 0, "Debug", fullMsg );
#endif
        }
    }
}
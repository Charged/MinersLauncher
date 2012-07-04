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
using System.Threading;
using System.IO;

namespace ChargedMinersLauncher {
    enum DialogState {
        Initializing,
        PlatformNotSupportedError,
        PromptingToDownload,
        PromptingToUpdate,
        DownloadingBinary,
        AtSignInForm,
        SigningIn
    };


    public sealed partial class MainForm : Form {
        readonly BackgroundWorker signInWorker = new BackgroundWorker(),
                                  versionCheckWorker = new BackgroundWorker();

        WebClient binaryDownloader = new WebClient();

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex =
                new Regex(
                    @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                    RegexOptions.IgnoreCase );

        string storedLoginUsername,
               storedMinecraftUsername;


        public MainForm() {
            InitializeComponent();

            State = DialogState.Initializing;

            signInWorker.DoWork += SignIn;
            signInWorker.RunWorkerCompleted += OnSignInCompleted;
            signInWorker.WorkerSupportsCancellation = true;

            versionCheckWorker.DoWork += CheckUpdates;
            versionCheckWorker.RunWorkerCompleted += OnCheckUpdatesCompleted;

            binaryDownloader.DownloadProgressChanged += OnDownloadProgress;
            binaryDownloader.DownloadFileCompleted += OnDownloadCompleted;
        }


        protected override void OnShown( EventArgs e ) {
            if( Paths.Init() ) {
                LoadLoginInfo();
                versionCheckWorker.RunWorkerAsync();
            } else {
                State = DialogState.PlatformNotSupportedError;
                tabs.SelectedTab = tabProgress;
            }
            base.OnShown( e );
        }


        string localHashString;
        static readonly Uri UpdateUri = new Uri( "http://cloud.github.com/downloads/Wallbraker/Charged-Miners/" );


        void CheckUpdates( object sender, DoWorkEventArgs e ) {
            if( File.Exists( Paths.ChargeBinary ) ) {
                MD5 hasher = MD5.Create();
                using( FileStream fs = File.OpenRead( Paths.ChargeBinary ) ) {
                    StringBuilder sb = new StringBuilder( 32 );
                    foreach( byte b in hasher.ComputeHash( fs ) ) {
                        sb.AppendFormat( "{0:x2}", b );
                    }
                    localHashString = sb.ToString();
                }
            }
            WebClient updateCheckDownloader = new WebClient();
            string versions = updateCheckDownloader.DownloadString( UpdateUri + "version.txt" );
            VersionsTxt versionList = new VersionsTxt( versions.Split( '\n' ) );
            latestVersion = versionList.Get( Paths.ChargeBinary );
        }


        void OnCheckUpdatesCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            if( latestVersion == null ) {
                State = DialogState.PlatformNotSupportedError;
            } else if( !File.Exists( Paths.ChargeBinary ) ) {
                State = DialogState.PromptingToDownload;
            } else if( !latestVersion.Md5.Equals( localHashString, StringComparison.OrdinalIgnoreCase ) ) {
                State = DialogState.PromptingToUpdate;
            } else {
                State = DialogState.AtSignInForm;
            }
        }


        #region Download

        void bDownloadNo_Click( object sender, EventArgs e ) {
            Environment.ExitCode = 1;
            Application.Exit();
        }


        VersionInfo latestVersion;


        void DownloadBegin() {
            State = DialogState.DownloadingBinary;
            binaryDownloader.DownloadFileAsync( new Uri( UpdateUri + latestVersion.HttpName ),
                                                Paths.ChargeBinary + ".tmp" );
        }


        void bDownloadYes_Click( object sender, EventArgs e ) {
            DownloadBegin();
        }


        void OnDownloadProgress( object sender, DownloadProgressChangedEventArgs e ) {
            pbSigningIn.Value = e.ProgressPercentage;
            lStatus.Text = String.Format( "Downloading {0} ({1}/{2})",
                                          Paths.ChargeBinary,
                                          e.BytesReceived,
                                          e.TotalBytesToReceive );
        }


        void OnDownloadCompleted( object sender, AsyncCompletedEventArgs e ) {
            if( File.Exists( Paths.ChargeBinary ) ) {
                File.Delete( Paths.ChargeBinary + ".old" );
                File.Replace( Paths.ChargeBinary + ".tmp", Paths.ChargeBinary, Paths.ChargeBinary + ".old", true );
            } else {
                File.Move( Paths.ChargeBinary + ".tmp", Paths.ChargeBinary );
            }
            State = DialogState.AtSignInForm;
        }

        #endregion


        #region SignIn

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

            State = DialogState.SigningIn;
            signInWorker.RunWorkerAsync();
        }


        void OnSignInCompleted( object sender, RunWorkerCompletedEventArgs e ) {
            if( e.Cancelled ) {
                tabs.SelectedTab = tabSignIn;
                return;
            }
            switch( MinecraftNetSession.Instance.Status ) {
                case LoginResult.Success:
                    SaveLoginInfo();
                    Process.Start( Paths.ChargeBinary, "PLAY_SESSION=" + MinecraftNetSession.Instance.PlaySessionCookie );
                    Application.Exit();
                    break;
                case LoginResult.WrongUsernameOrPass:
                    WarningForm.Show( "Could not sign in", "Wrong username or password." );
                    tabs.SelectedTab = tabSignIn;
                    break;
                case LoginResult.Error:
                    Exception ex = MinecraftNetSession.Instance.LoginException;
                    if( ex != null ) {
                        WarningForm.Show( "Could not sign in", ex.Message );
                    } else {
                        WarningForm.Show( "Could not sign in", "An unknown error occured." );
                    }
                    tabs.SelectedTab = tabSignIn;
                    break;
            }
        }


        void LoadLoginInfo() {
            string passwordFileFullName = Path.Combine( Paths.ConfigPath, Paths.PasswordSaveFile );
            try {
                if( File.Exists( passwordFileFullName ) ) {
                    string[] loginData = File.ReadAllLines( passwordFileFullName );
                    storedLoginUsername = loginData[0];
                    tUsername.Text = storedLoginUsername;
                    tPassword.Text = loginData[1];
                    storedMinecraftUsername = loginData.Length > 2 ? loginData[2] : storedLoginUsername;
                    xRemember.Checked = true;
                }
            } catch( Exception ex ) {
                WarningForm.Show( "Error loading saved login", ex.ToString() );
            }
        }


        void SaveLoginInfo() {
            string passwordFileFullName = Path.Combine( Paths.ConfigPath, Paths.PasswordSaveFile );
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


        void SignIn( object sender, DoWorkEventArgs e ) {
            try {
                MinecraftNetSession.Instance.Login( xRemember.Checked );
            } catch( WebException ex ) {
                MinecraftNetSession.Instance.LoginException = ex;
                MinecraftNetSession.Instance.Status = LoginResult.Error;
            }
        }

        #endregion


        #region UI

        void bCancel_Click( object sender, EventArgs e ) {
            switch( State ) {
                case DialogState.SigningIn:
                    signInWorker.CancelAsync();
                    lStatus2.Text = "Canceling...";
                    break;
                case DialogState.PlatformNotSupportedError:
                    Environment.ExitCode = 1;
                    Application.Exit();
                    break;
            }
        }


        DialogState State {
            get { return state; }
            set {
                switch( value ) {
                    case DialogState.Initializing:
                        lStatus.Text = "Initializing...";
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Marquee;
                        bCancel.Visible = false;
                        break;

                    case DialogState.PlatformNotSupportedError:
                        lStatus.Text = "Failed to initialize";
                        lStatus2.Text = "No binaries are available for your platform.";
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 100;
                        bCancel.Text = "OK";
                        bCancel.Visible = true;
                        break;

                    case DialogState.PromptingToDownload:
                        tabs.SelectedTab = tabDownload;
                        break;

                    case DialogState.PromptingToUpdate:
                        tabs.SelectedTab = tabUpdate;
                        break;

                    case DialogState.DownloadingBinary:
                        lStatus.Text = String.Format( "Downloading {0} (?/?)", Paths.ChargeBinary );
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 0;
                        tabs.SelectedTab = tabProgress;
                        bCancel.Visible = false;
                        break;

                    case DialogState.AtSignInForm:
                        tabs.SelectedTab = tabSignIn;
                        break;

                    case DialogState.SigningIn:
                        lStatus.Text = String.Format( "Signing in as {0}...",
                                                      MinecraftNetSession.Instance.LoginUsername );
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Marquee;
                        bCancel.Text = "Cancel";
                        bCancel.Visible = true;
                        tabs.SelectedTab = tabProgress;
                        break;
                }
                state = value;
            }
        }

        DialogState state;

        #endregion


        void bUpdateYes_Click( object sender, EventArgs e ) {
            DownloadBegin();
        }


        void bUpdateNo_Click( object sender, EventArgs e ) {
            State = DialogState.AtSignInForm;
        }
    }
}
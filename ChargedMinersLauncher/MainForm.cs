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
    public sealed partial class MainForm : Form {
        readonly BackgroundWorker signInWorker = new BackgroundWorker(),
                                  versionCheckWorker = new BackgroundWorker();

        string localHashString;
        static readonly Uri UpdateUri = new Uri( "http://cloud.github.com/downloads/Wallbraker/Charged-Miners/" );

        string storedLoginUsername,
               storedMinecraftUsername;


        public MainForm() {
            InitializeComponent();

            State = FormState.Initializing;

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
                State = FormState.PlatformNotSupportedError;
                tabs.SelectedTab = tabProgress;
            }
            base.OnShown( e );
        }


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
                State = FormState.PlatformNotSupportedError;
            } else if( !File.Exists( Paths.ChargeBinary ) ) {
                State = FormState.PromptingToDownload;
            } else if( !latestVersion.Md5.Equals( localHashString, StringComparison.OrdinalIgnoreCase ) ) {
                State = FormState.PromptingToUpdate;
            } else {
                State = FormState.AtSignInForm;
            }
        }


        #region Download

        readonly WebClient binaryDownloader = new WebClient();
        VersionInfo latestVersion;

        void bDownloadNo_Click( object sender, EventArgs e ) {
            Environment.ExitCode = 1;
            Application.Exit();
        }


        void DownloadBegin() {
            State = FormState.DownloadingBinary;
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
            State = FormState.AtSignInForm;
        }

        #endregion


        #region SignIn

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
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
                    lSignInStatus.Text = "Wrong username or password.";
                    tabs.SelectedTab = tabSignIn;
                    break;
                case LoginResult.Error:
                    Exception ex = MinecraftNetSession.Instance.LoginException;
                    if( ex != null ) {
                        lSignInStatus.Text = "Error: " + ex.Message;
                    } else {
                        lSignInStatus.Text = "An unknown error occured.";
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
            } catch( Exception ) {
                lSignInStatus.Text = "Could not load saved login information.";
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
                case FormState.SigningIn:
                    signInWorker.CancelAsync();
                    lStatus2.Text = "Canceling...";
                    break;
                case FormState.PlatformNotSupportedError:
                    Environment.ExitCode = 1;
                    Application.Exit();
                    break;
            }
        }


        FormState State {
            get { return state; }
            set {
                switch( value ) {
                    case FormState.Initializing:
                        lSignInStatus.Text = "";
                        lStatus.Text = "Initializing...";
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Marquee;
                        bCancel.Visible = false;
                        break;

                    case FormState.PlatformNotSupportedError:
                        lStatus.Text = "Failed to initialize";
                        lStatus2.Text = "No binaries are available for your platform.";
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 100;
                        bCancel.Text = "OK";
                        bCancel.Visible = true;
                        break;

                    case FormState.PromptingToDownload:
                        tabs.SelectedTab = tabDownload;
                        break;

                    case FormState.PromptingToUpdate:
                        tabs.SelectedTab = tabUpdate;
                        break;

                    case FormState.DownloadingBinary:
                        lStatus.Text = String.Format( "Downloading {0} (?/?)", Paths.ChargeBinary );
                        lStatus2.Text = "";
                        pbSigningIn.Style = ProgressBarStyle.Continuous;
                        pbSigningIn.Value = 0;
                        tabs.SelectedTab = tabProgress;
                        bCancel.Visible = false;
                        break;

                    case FormState.AtSignInForm:
                        tabs.SelectedTab = tabSignIn;
                        break;

                    case FormState.SigningIn:
                        lSignInStatus.Text = "";
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

        FormState state;


        void bUpdateYes_Click( object sender, EventArgs e ) {
            DownloadBegin();
        }


        void bUpdateNo_Click( object sender, EventArgs e ) {
            State = FormState.AtSignInForm;
        }

        #endregion
    }
}
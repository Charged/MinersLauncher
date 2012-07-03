// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ChargedMinersLauncher {
    public sealed partial class SignInForm : Form {
        readonly BackgroundWorker signInWorker = new BackgroundWorker();

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                                    RegexOptions.IgnoreCase );

        string storedLoginUsername,
               storedMinecraftUsername;


        public SignInForm() {
            InitializeComponent();
            if( !Paths.Init() ) {
                Environment.ExitCode = 1;
                Application.Exit();
                return;
            }

            LoadLoginInfo();

            signInWorker.DoWork += SignIn;
            signInWorker.RunWorkerCompleted += OnSignInCompleted;
            signInWorker.WorkerSupportsCancellation = true;
        }


        void OnTextChanged( object sender, EventArgs e ) {
            if( UsernameRegex.IsMatch( tUsername.Text ) || EmailRegex.IsMatch( tUsername.Text ) ) {
                tUsername.BackColor = SystemColors.Window;
            } else {
                tUsername.BackColor = Color.Yellow;
            }
            bSignIn.Enabled = ( tPassword.Text.Length > 0 );
        }


        private void bCancel_Click( object sender, EventArgs e ) {
            lSigningIn.Text = "Canceling...";
            bCancel.Enabled = false;
            signInWorker.CancelAsync();
        }


        void bSignIn_Click( object sender, EventArgs e ) {
            string minecraftUsername;
            if( tUsername.Text == storedLoginUsername ) {
                minecraftUsername = storedMinecraftUsername;
            } else {
                minecraftUsername = tUsername.Text;
            }
            MinecraftNetSession.Instance = new MinecraftNetSession( tUsername.Text, minecraftUsername, tPassword.Text );

            bCancel.Enabled = true;
            lSigningIn.Text = String.Format( "Signing in as {0}...", MinecraftNetSession.Instance.LoginUsername );
            tabs.SelectedTab = tabSignInProgress;

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
                    // TODO: Check for ChargedMiners update
                    lSigningIn.Text = "Checking for ChargedMiners updates...";
                    break;
                case LoginResult.WrongUsernameOrPass:
                    WarningForm.Show( "Could not sign in", "Wrong username or password." );
                    tabs.SelectedTab = tabSignIn;
                    break;
                case LoginResult.Error:
                    WarningForm.Show( "Could not sign in", MinecraftNetSession.Instance.LoginException.Message );
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


        void SignIn( object sender, DoWorkEventArgs e ){
            try {
                MinecraftNetSession.Instance.Login( xRemember.Checked );
            } catch( WebException ex ) {
                MinecraftNetSession.Instance.LoginException = ex;
                MinecraftNetSession.Instance.Status = LoginResult.Error;
            }
        }
    }
}
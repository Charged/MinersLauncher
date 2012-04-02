using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ChargedMinersLauncher {
    public sealed partial class SignInForm : Form {
        ServerInfo[] servers;
        public const string ChargeBinary = "Charge.exe";
        const string PasswordSaveFile = "saved-login.dat";

        static readonly Regex UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" );

        static readonly Regex EmailRegex =
            new Regex(
                @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" );


        public SignInForm() {
            if( !File.Exists( ChargeBinary ) ) {
                MessageBox.Show( "Charge.exe not found!" );
            }
            InitializeComponent();
            string passwordFileFullName = Path.Combine( ChargedMinersSettings.ConfigPath, PasswordSaveFile );
            try {
                if( File.Exists( passwordFileFullName ) ) {
                    string[] loginData = File.ReadAllLines( passwordFileFullName );
                    tUsername.Text = loginData[0];
                    tPassword.Text = loginData[1];
                    xRemember.Checked = true;
                }
            } catch( Exception ex ) {
                MessageBox.Show( ex.ToString(), "Error loading saved login" );
            }
        }


        void OnTextChanged( object sender, EventArgs e ) {
            bool valid = true;
            if( UsernameRegex.IsMatch( tUsername.Text ) ) {
                tUsername.BackColor = SystemColors.Window;
            } else if( EmailRegex.IsMatch( tUsername.Text ) ) {
                tUsername.BackColor = SystemColors.Window;
            } else {
                tUsername.BackColor = Color.Yellow;
                valid = false;
            }
            if( tPassword.Text.Length == 0 ) {
                valid = false;
            }
            bSignIn.Enabled = valid;
        }


        void bSignIn_Click( object sender, EventArgs e ) {
            string passwordFileFullName = Path.Combine( ChargedMinersSettings.ConfigPath, PasswordSaveFile );
            if( xRemember.Checked ) {
                if( !Directory.Exists( ChargedMinersSettings.ConfigPath ) ) {
                    Directory.CreateDirectory( ChargedMinersSettings.ConfigPath );
                }
                File.WriteAllLines( passwordFileFullName, new[] { tUsername.Text, tPassword.Text } );
            } else {
                if( File.Exists( passwordFileFullName ) ) {
                    File.Delete( passwordFileFullName );
                }
            }

            MinecraftNetSession.Instance = new MinecraftNetSession( tUsername.Text, tPassword.Text );
            LoadingForm progressBox = new LoadingForm( "Signing into minecraft.net" );
            progressBox.Shown += ( s2, e2 ) => ThreadPool.QueueUserWorkItem( SignIn, progressBox );
            progressBox.ShowDialog();
            if( MinecraftNetSession.Instance.Status == LoginResult.Success ) {
                Hide();
                new ServerListForm( servers ).ShowDialog();
                Application.Exit();
            }
        }


        void SignIn( object param ) {
            LoadingForm progressBox = (LoadingForm)param;
            try {
                switch( MinecraftNetSession.Instance.Login( xRemember.Checked ) ) {
                    case LoginResult.Success:
                        progressBox.SetText( "Loading server list" );
                        servers = MinecraftNetSession.Instance.GetServerList();
                        break;
                    case LoginResult.WrongUsernameOrPass:
                        MessageBox.Show( "Wrong username or password.", "Could not sign in" );
                        break;
                    case LoginResult.Error:
                        MessageBox.Show( "An unknown error occured.", "Could not sign in" );
                        break;
                }
            } catch( WebException ex ) {
                MinecraftNetSession.Instance.Status = LoginResult.Error;
                MessageBox.Show( ex.Message,
                                 "Could not sign in" );
            } finally {
                progressBox.Invoke( (Action)progressBox.Close );
            }
        }
    }
}
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ChargedMinersLauncher {
    public sealed partial class SignInForm : Form {
        ServerInfo[] servers;
        public const string ChargeBinary = "Charge.exe";
        const string PasswordSaveFile = "saved-login.dat";

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


        static bool IsValidName( string name ) {
            if( name == null ) throw new ArgumentNullException( "name" );
            if( name.Length < 2 || name.Length > 16 ) return false;
            return name.All( ch => ( ch >= '0' || ch == '.' ) &&
                                   ( ch <= '9' || ch >= 'A' ) && ( ch <= 'Z' || ch >= '_' ) &&
                                   ( ch <= '_' || ch >= 'a' ) && ch <= 'z' );
        }


        private void OnTextChanged( object sender, EventArgs e ) {
            bool valid = true;
            if( IsValidName( tUsername.Text ) ) {
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

        private void bSignIn_Click( object sender, EventArgs e ) {
            string passwordFileFullName = Path.Combine( ChargedMinersSettings.ConfigPath, PasswordSaveFile );
            if( xRemember.Checked ) {
                if( !Directory.Exists( ChargedMinersSettings.ConfigPath ) ) {
                    Directory.CreateDirectory( ChargedMinersSettings.ConfigPath );
                }
                File.WriteAllLines( passwordFileFullName, new[] { tUsername.Text, tPassword.Text } );
            }else{
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


        private void SignIn( object param ) {
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

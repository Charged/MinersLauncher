using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ChargedMinersLauncher {
    public partial class LoginForm : Form {
        ServerInfo[] servers;
        public const string ChargeBinary = "Charge.exe";
        const string PasswordSaveFile = "saved-login.dat";

        public static LoginForm Instance { get; private set; }

        public LoginForm() {
            if( !File.Exists( ChargeBinary ) ) {
                MessageBox.Show( "Charge.exe not found!" );
                Application.Exit();
            }
            InitializeComponent();
            Instance = this;
            try {
                if( File.Exists( PasswordSaveFile ) ) {
                    string[] loginData = File.ReadAllLines( PasswordSaveFile );
                    tUsername.Text = loginData[0];
                    tPassword.Text = loginData[1];
                    xRemember.Checked = true;
                }
            } catch( Exception ex ) {
                MessageBox.Show( ex.ToString(), "Error loading saved login" );
            }
        }


        public static bool IsValidName( string name ) {
            if( name == null ) throw new ArgumentNullException( "name" );
            if( name.Length < 2 || name.Length > 16 ) return false;
            for( int i = 0; i < name.Length; i++ ) {
                char ch = name[i];
                if( (ch < '0' && ch != '.') || (ch > '9' && ch < 'A') || (ch > 'Z' && ch < '_') || (ch > '_' && ch < 'a') || ch > 'z' ) {
                    return false;
                }
            }
            return true;
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
            if( xRemember.Checked ) {
                File.WriteAllLines( PasswordSaveFile, new[] { tUsername.Text, tPassword.Text } );
            }else{
                if( File.Exists( PasswordSaveFile ) ) {
                    File.Delete( PasswordSaveFile );
                }
            }

            MinecraftNetSession.Instance = new MinecraftNetSession( tUsername.Text, tPassword.Text );
            LoadingForm progressBox = new LoadingForm( "Signing into minecraft.net" );
            progressBox.Shown += delegate( object s2, EventArgs e2 ) {
                ThreadPool.QueueUserWorkItem( SignIn, progressBox );
            };
            progressBox.ShowDialog();
            if( servers != null ) {
                Hide();
                new ServerListForm( servers ).ShowDialog();
                Application.Exit();
            } else {
                MessageBox.Show( MinecraftNetSession.Instance.Status.ToString() );
            }
        }


        private void SignIn( object param ) {
            LoadingForm progressBox = (LoadingForm)param;
            MinecraftNetSession.Instance.Login();
            if( MinecraftNetSession.Instance.Status != LoginResult.Success ) {
                progressBox.Invoke( (Action)progressBox.Close );
                return;
            }
            progressBox.SetText( "Loading server list" );
            servers = MinecraftNetSession.Instance.GetServerList();
            progressBox.Invoke( (Action)progressBox.Close );
        }
    }
}

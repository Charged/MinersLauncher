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

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                                    RegexOptions.IgnoreCase );

        readonly string storedLoginUsername,
                        storedMinecraftUsername;


        public SignInForm() {
            if( !File.Exists( ChargeBinary ) ) {
                WarningForm.Show( "Warning", "Charge.exe not found!" );
            }
            InitializeComponent();
            string passwordFileFullName = Path.Combine( ChargedMinersSettings.ConfigPath, PasswordSaveFile );
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
                WarningForm.Show( "Error loading saved login",  ex.ToString() );
            }
        }


        void OnTextChanged( object sender, EventArgs e ) {
            if( UsernameRegex.IsMatch( tUsername.Text ) || EmailRegex.IsMatch( tUsername.Text ) ) {
                tUsername.BackColor = SystemColors.Window;
            } else {
                tUsername.BackColor = Color.Yellow;
            }
            bSignIn.Enabled = (tPassword.Text.Length >0);
        }


        void bSignIn_Click( object sender, EventArgs e ) {
            string minecraftUsername;
            if( tUsername.Text == storedLoginUsername ) {
                minecraftUsername = storedMinecraftUsername;
            } else {
                minecraftUsername = tUsername.Text;
            }
            MinecraftNetSession.Instance = new MinecraftNetSession( tUsername.Text, minecraftUsername, tPassword.Text );
            LoadingForm loadingForm = new LoadingForm( "Signing into minecraft.net" );
            loadingForm.Shown += ( s2, e2 ) => ThreadPool.QueueUserWorkItem( SignIn, loadingForm );
            loadingForm.ShowDialog();
            if( MinecraftNetSession.Instance.Status == LoginResult.Success ) {
                string passwordFileFullName = Path.Combine( ChargedMinersSettings.ConfigPath, PasswordSaveFile );
                if( xRemember.Checked ) {
                    if( !Directory.Exists( ChargedMinersSettings.ConfigPath ) ) {
                        Directory.CreateDirectory( ChargedMinersSettings.ConfigPath );
                    }
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
                        WarningForm.Show( "Could not sign in", "Wrong username or password." );
                        break;
                    case LoginResult.Error:
                        WarningForm.Show( "Could not sign in", "An unknown error occured." );
                        break;
                }
            } catch( WebException ex ) {
                MinecraftNetSession.Instance.Status = LoginResult.Error;
                WarningForm.Show( "Could not sign in", ex.Message );
            } finally {
                progressBox.Invoke( (Action)progressBox.Close );
            }
        }

        private void bSettings_Click( object sender, EventArgs e ) {
            new SettingsForm().ShowDialog();
        }
    }
}
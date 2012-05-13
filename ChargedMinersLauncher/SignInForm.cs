﻿// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ChargedMinersLauncher {
    public sealed partial class SignInForm : Form {
        ServerInfo[] servers;
        public static string ChargeBinary;
        public static string ChargeBinaryAlt;
        public const string ChargeBinaryFormatWindows = "Charge.{0}.exe";
        public const string ChargeBinaryFormatMacOSX = "Charge.{0}.MacOSX";
        public const string ChargeBinaryFormatLinux = "Charge.{0}.Linux";
        public const string ChargeBinaryFormat32Bit = "i386";
        public const string ChargeBinaryFormat64Bit = "x86_64";
        const string PasswordSaveFile = "saved-login.dat";

        static readonly Regex
            UsernameRegex = new Regex( @"^[a-zA-Z0-9_\.]{2,16}$" ),
            EmailRegex = new Regex( @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
                                    RegexOptions.IgnoreCase );

        readonly string storedLoginUsername,
                        storedMinecraftUsername;


        public SignInForm() {

            string tmp;
            if( RuntimeInfo.IsWindows ) {
                tmp = ChargeBinaryFormatWindows;
            } else if( RuntimeInfo.IsMacOSX ) {
                tmp = ChargeBinaryFormatMacOSX;
            } else if( RuntimeInfo.IsLinux ) {
                tmp = ChargeBinaryFormatLinux;
            } else {
                // XXX Something better here.
                tmp = null;
            }

            if( RuntimeInfo.Is32Bit ) {
                ChargeBinary = String.Format( tmp, ChargeBinaryFormat32Bit );
                ChargeBinaryAlt = "";
            } else if ( RuntimeInfo.Is64Bit ) {
                ChargeBinary = String.Format( tmp, ChargeBinaryFormat64Bit );
                ChargeBinaryAlt = String.Format( tmp, ChargeBinaryFormat32Bit );
            }

            // XXX This code should be moved into the auto updater.
            if( !File.Exists( ChargeBinary ) ) {
                // Look for both.
                if ( !File.Exists( ChargeBinary ) ) {
                    WarningForm.Show( "Warning", String.Format( "The binray \"{0}\" not found!", ChargeBinary ) );
                } else {
                    ChargeBinary = ChargeBinaryAlt;
                }
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
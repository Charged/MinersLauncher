using System;
using System.Collections.Generic;
using System.IO;

namespace ChargedMinersLauncher {
    class AccountManager {
        readonly Dictionary<string, SignInAccount> storedAccounts = new Dictionary<string, SignInAccount>();

        public void AddAccount( SignInAccount newAccount ) {
            storedAccounts.Add( newAccount.SignInUsername.ToLowerInvariant(), newAccount );
            SaveAccounts();
        }


        public void RemoveAccount( SignInAccount account ) {
            storedAccounts.Remove( account.SignInUsername.ToLower() );
            SaveAccounts();
        }


        public void RemoveAllAccounts() {
            string[] fileNames = Directory.GetFiles( Paths.DataDirectory, "*.account" );
            foreach( string fileName in fileNames ) {
                File.Delete( fileName );
            }
        }


        public void LoadAccounts() {
            storedAccounts.Clear();
            SettingsFile sf = new SettingsFile();
            string[] fileNames = Directory.GetFiles( Paths.DataDirectory, "*.account" );
            foreach( string fileName in fileNames ) {
                sf.Load( fileName );
                SignInAccount newAccount = new SignInAccount {
                    SignInUsername = sf.GetString( "SignInUsername", "" ),
                    PlayerName = sf.GetString( "PlayerName", "" ),
                    Password = sf.GetString( "Password", "" ),
                    LastUrl = sf.GetString( "LastUrl", "" )
                };
                string tickString = sf.GetString( "SignInDate", "0" );
                long ticks;
                if( Int64.TryParse( tickString, out ticks ) &&
                    ticks > DateTime.MinValue.Ticks &&
                    ticks <= DateTime.MaxValue.Ticks ) {
                    newAccount.SignInDate = new DateTime( ticks );
                } else {
                    newAccount.SignInDate = DateTime.MinValue;
                }
                AddAccount( newAccount );
            }
            SaveAccounts();
        }


        public void SaveAccounts() {
            SettingsFile sf = new SettingsFile();
            foreach( SignInAccount account in storedAccounts.Values ) {
                sf.Set( "SignInUsername", account.SignInUsername );
                sf.Set( "PlayerName", account.PlayerName );
                sf.Set( "Password", PasswordSecurity.EncryptPassword( account.Password ) );
                sf.Set( "LastUrl", account.LastUrl );
                sf.Set( "SignInDate", account.SignInDate.Ticks );
                sf.Save( account.FileName );
            }
        }
    }
}

// Part of ChargedMinersLauncher | Copyright (c) 2012-2013 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChargedMinersLauncher {
    class AccountManager {
        readonly Dictionary<string, SignInAccount> storedAccounts = new Dictionary<string, SignInAccount>();


        public int Count {
            get {
                return storedAccounts.Count;
            }
        }


        public void AddAccount( SignInAccount newAccount ) {
            storedAccounts.Add( newAccount.SignInUsername.ToLowerInvariant(), newAccount );
        }


        public bool HasAccount( string signInUsername ) {
            return storedAccounts.ContainsKey( signInUsername.ToLowerInvariant() );
        }


        public void RemoveAccount( SignInAccount account ) {
            storedAccounts.Remove( account.SignInUsername.ToLower() );
            RemoveAllAccountFiles();
            SaveAllAccounts();
        }


        public void RemoveAllAccounts() {
            storedAccounts.Clear();
            RemoveAllAccountFiles();
        }


        public void RemoveAllAccountFiles() {
            string[] fileNames = Directory.GetFiles( Paths.DataDirectory, "*.account" );
            foreach( string fileName in fileNames ) {
                File.Delete( fileName );
            }
        }

        public void LoadAccounts() {
            storedAccounts.Clear();
            string[] fileNames = Directory.GetFiles( Paths.DataDirectory, "*.account" );
            foreach( string fileName in fileNames ) {
                try {
                    SettingsFile sf = new SettingsFile();
                    sf.Load( fileName );
                    SignInAccount newAccount = new SignInAccount {
                        SignInUsername = sf.GetString( "SignInUsername", "" ),
                        PlayerName = sf.GetString( "PlayerName", "" ),
                        Password = sf.GetString( "Password", "" ),
                        LastUrl = sf.GetString( "LastUrl", "" )
                    };
                    if( newAccount.Password.Length > 0 ) {
                        newAccount.Password = PasswordSecurity.DecryptPassword( newAccount.Password );
                    }
                    string tickString = sf.GetString( "SignInDate", "0" );
                    long ticks;
                    if( Int64.TryParse( tickString, out ticks ) && ticks > DateTime.MinValue.Ticks &&
                        ticks <= DateTime.MaxValue.Ticks ) {
                        newAccount.SignInDate = new DateTime( ticks );
                    } else {
                        newAccount.SignInDate = DateTime.MinValue;
                    }
                    AddAccount( newAccount );
                } catch( Exception ex ) {
                    MainForm.Log( "AccountManager.LoadAccounts: " + ex );
                }
            }
            SaveAllAccounts();
        }


        public void SaveAllAccounts() {
            RemoveAllAccountFiles();
            foreach( SignInAccount account in storedAccounts.Values ) {
                account.Save();
            }
        }


        public SignInAccount FindAccount( string signInName ) {
            SignInAccount acct;
            if( storedAccounts.TryGetValue( signInName.ToLowerInvariant(), out acct ) ) {
                return acct;
            } else {
                return null;
            }
        }


        public SignInAccount[] GetAccountsBySignInDate() {
            return storedAccounts.Values
                                 .OrderByDescending( acct => acct.SignInDate )
                                 .ToArray();
        }


        public SignInAccount GetMostRecentlyUsedAccount() {
            SignInAccount account = GetAccountsBySignInDate().FirstOrDefault();
            if( Count > 1 ) {
                var otherAccounts = storedAccounts.Where( a => a.Value != account ).ToArray();
                foreach( var otherAccount in otherAccounts ) {
                    storedAccounts.Remove( otherAccount.Key );
                }
            }
            return account;
        }
    }
}

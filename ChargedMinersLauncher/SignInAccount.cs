// Part of ChargedMinersLauncher | Copyright (c) 2012-2013 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;

namespace ChargedMinersLauncher {
    sealed class SignInAccount {
        public string SignInUsername { get; set; }
        public string PlayerName { get; set; }
        public string Password { get; set; }
        public string LastUrl { get; set; }
        public DateTime SignInDate { get; set; }

        public string FileName {
            get {
                return SignInUsername.ToLowerInvariant() + ".account";
            }
        }

        public void Save() {
            SettingsFile sf = new SettingsFile();
            sf.Set( "SignInUsername", SignInUsername );
            sf.Set( "PlayerName", PlayerName );
            sf.Set( "Password", PasswordSecurity.EncryptPassword( Password ) );
            sf.Set( "LastUrl", LastUrl );
            sf.Set( "SignInDate", SignInDate.Ticks );
            sf.Save( FileName );
        }
    }
}
// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;

sealed class SignInAccount {
    public string SignInUsername { get; set; }
    public string PlayerName { get; set; }
    public string Password { get; set; }
    public string LastUrl { get; set; }
    public DateTime SignInDate { get; set; }

    public string FileName {
        get {
            return SignInUsername + ".account";
        }
    }
}
// Part of ChargedMinersLauncher | Copyright (c) 2012-2013 Jakob Bornecrantz <wallbraker@gmail.com> | BSD-3 | See LICENSE.txt
using System;

namespace ChargedMinersLauncher {
    sealed class VersionInfo {
        public string Name { get; private set; }
        public string HttpName { get; private set; }
        public string Md5 { get; private set; }


        public VersionInfo( string name, string httpName, string md5 ) {
            Name = name;
            HttpName = httpName;
            Md5 = md5.ToLower();

            if( Md5.Length != 32 ) {
                throw new ArgumentException( "Not a valid md5 string array" );
            }
        }
    }
}
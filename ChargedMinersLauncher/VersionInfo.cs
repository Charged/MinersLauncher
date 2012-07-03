// Part of ChargedMinersLaunher | Copyright (c) 2012 Jakob Bornecrantz <wallbraker@gmail.com> | BSD-3 | See LICENSE.txt
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


        public bool CompareHash( byte[] other ) {
            var str = BitConverter.ToString( other ).Replace( "-", "" ).ToLower();
            return String.Equals( Md5, str, StringComparison.OrdinalIgnoreCase );
        }


        public bool CompareHash( string other ) {
            return String.Equals( Md5, other, StringComparison.OrdinalIgnoreCase );
        }
    }
}
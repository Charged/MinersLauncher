// Part of ChargedMinersLauncher | Copyright (c) 2012 Jakob Bornecrantz <wallbraker@gmail.com> | BSD-3 | See LICENSE.txt
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    sealed class VersionsTxt {
        static readonly Regex CommentRegex = new Regex( @"^\s*#" );
        static readonly Regex NameRegex = new Regex( @"^(\S+)\s*$" );
        static readonly Regex MD5AndNameRegex = new Regex( @"\s*([a-fA-F0-9]{32})\s+(\S+)\s*$" );

        readonly Dictionary<string, VersionInfo> versions = new Dictionary<string, VersionInfo>();


        public VersionsTxt( IEnumerable<string> lines ) {
            string name = "";
            bool nameFound = false;
            foreach( string line in lines ) {
                if( line == null ) break;
                if( line.Length == 0 || CommentRegex.IsMatch( line ) ) continue;

                var match = NameRegex.Match( line );
                if( match.Success ) {
                    nameFound = true;
                    name = match.Groups[1].Value;
                    continue;
                }

                match = MD5AndNameRegex.Match( line );
                if( match.Success && nameFound ) {
                    nameFound = false;

                    var md5 = match.Groups[1].Value;
                    var httpName = match.Groups[2].Value;

                    versions.Add( name, new VersionInfo( name, httpName, md5 ) );
                }
            }
        }


        public VersionInfo Get( string name ) {
            VersionInfo ret;
            if( versions.TryGetValue( name, out ret ) ) {
                return ret;
            } else {
                return null;
            }
        }
    }
}
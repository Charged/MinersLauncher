// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    static class SettingsFile {
        static readonly Regex CommentRegex = new Regex( @"^\s*#" );
        static readonly Regex SettingRegex = new Regex( @"^\s*(\S*)\s*=\s*(\S*)" );
        static readonly Regex SettingLiteralRegex = new Regex( @"\s*(\S*)\s*=?\s*:(.*)" );

        public static Dictionary<string, string> Load( string fileName ) {
            string[] lines = File.ReadAllLines(fileName);
            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach( string line in lines ) {
                if( line == null ) break;
                if( line.Length == 0 || CommentRegex.IsMatch( line ) ) continue;
                var match = SettingLiteralRegex.Match( line );
                if( match.Success ) {
                    values.Add( match.Groups[1].Value, match.Groups[2].Value );
                }
                match = SettingRegex.Match( line );
                if( match.Success ) {
                    values.Add( match.Groups[1].Value, match.Groups[2].Value );
                }
            }
            return values;
        }


        public static void Save( string fileName, Dictionary<string, string> values ) {
            using( StreamWriter sw = new StreamWriter( fileName ) ) {
                foreach( var kvp in values ) {
                    sw.Write( kvp.Key + ':' + kvp.Value + '\n' );
                }
            }
        }
    }
}
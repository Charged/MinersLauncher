// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    sealed class SettingsFile {
        static readonly Regex CommentRegex = new Regex( @"^\s*#" );
        static readonly Regex SettingRegex = new Regex( @"^\s*(\S+?)\s*=\s*(\S*)$" );
        static readonly Regex SettingLiteralRegex = new Regex( @"^\s*(\S+?)\s*=?\s*:(.*)$" );

        readonly Dictionary<string, string> settings = new Dictionary<string, string>();


        public void Load( string fileName ) {
            string[] lines = File.ReadAllLines( fileName );
            foreach( string line in lines ) {
                if( line == null ) break;
                if( line.Length == 0 || CommentRegex.IsMatch( line ) ) continue;
                var match = SettingLiteralRegex.Match( line );
                if( match.Success ) {
                    settings.Add( match.Groups[1].Value, match.Groups[2].Value );
                }
                match = SettingRegex.Match( line );
                if( match.Success ) {
                    settings.Add( match.Groups[1].Value, match.Groups[2].Value );
                }
            }
        }


        public bool GetBool( string key, bool defaultVal ) {
            string stringVal;
            if( settings.TryGetValue( key, out stringVal ) ) {
                bool boolVal;
                if( Boolean.TryParse( stringVal, out boolVal ) ) {
                    return boolVal;
                }
            }
            return defaultVal;
        }


        public string GetString( string key, string defaultVal ) {
            string stringVal;
            if( settings.TryGetValue( key, out stringVal ) ) {
                return stringVal;
            }
            return defaultVal;
        }


        public TEnum GetEnum<TEnum>( string key, TEnum defaultVal ) where TEnum : struct {
            string stringVal;
            if( settings.TryGetValue( key, out stringVal ) ) {
                try {
                    TEnum enumVal = (TEnum)Enum.Parse( typeof( TEnum ), stringVal, true );
                    if( Enum.IsDefined( typeof( TEnum ), enumVal ) ) {
                        return enumVal;
                    }
                } catch( ArgumentException ) {} catch( OverflowException ) {}
            }
            return defaultVal;
        }


        public void Set( string key, object value ) {
            settings[key] = value.ToString();
        }


        public void Save( string fileName ) {
            using( StreamWriter sw = new StreamWriter( fileName ) ) {
                foreach( var kvp in settings ) {
                    sw.Write( kvp.Key + ':' + kvp.Value + '\n' );
                }
            }
        }
    }
}
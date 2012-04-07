// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    sealed class ChargedMinersSettings {
        public const string ConfigFileName = "settings.ini";

        public static string ConfigPath {
            get { return Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" ); }
        }

        public static string ConfigFileFullName {
            get { return Path.Combine( ConfigPath, ConfigFileName ); }
        }

        readonly Dictionary<string, string> unrecognizedValues = new Dictionary<string, string>();


        public int Width { get; set; }
        public int Height { get; set; }
        public bool ForceResizeEnable { get; set; }
        public bool Fullscreen { get; set; }
        public bool AntiAliasEnabled { get; set; }
        public bool FogEnabled { get; set; }
        public bool ShadowsEnabled { get; set; }
        public int ViewDistance { get; set; }

        static readonly Regex CommentRegex = new Regex( @"^\s*#" );
        static readonly Regex SettingRegex = new Regex( @"^\s*(\S*)\s*=\s*(\S*)" );
        static readonly Regex SettingLiteralRegex = new Regex( @"\s*(\S*)\s*=?\s*:(.*)" );


        public ChargedMinersSettings() {
            Width = 800;
            Height = 600;
            ForceResizeEnable = false;
            Fullscreen = false;
            AntiAliasEnabled = true;
            FogEnabled = true;
            ShadowsEnabled = true;
            ViewDistance = 256;
        }


        public ChargedMinersSettings( IEnumerable<string> lines )
            : this() {
            foreach( string line in lines ) {
                if( line == null ) break;
                if( line.Length == 0 || CommentRegex.IsMatch( line ) ) continue;
                var match = SettingLiteralRegex.Match( line );
                if( match.Success ) {
                    Set( match.Groups[1].Value, match.Groups[2].Value );
                }
                match = SettingRegex.Match( line );
                if( match.Success ) {
                    Set( match.Groups[1].Value, match.Groups[2].Value );
                }
            }
        }


        void Set( string key, string value ) {
            switch( key ) {
                case "w":
                    Width = Int32.Parse( value );
                    break;
                case "h":
                    Height = Int32.Parse( value );
                    break;
                case "forceResizeEnable":
                    ForceResizeEnable = Boolean.Parse( value );
                    break;
                case "fullscreen":
                    Fullscreen = Boolean.Parse( value );
                    break;
                case "mc.aa":
                    AntiAliasEnabled = Boolean.Parse( value );
                    break;
                case "mc.fog":
                    FogEnabled = Boolean.Parse( value );
                    break;
                case "mc.shadow":
                    ShadowsEnabled = Boolean.Parse( value );
                    break;
                case "mc.viewDistance":
                    ViewDistance = Int32.Parse( value );
                    break;
                default:
                    unrecognizedValues.Add( key, value );
                    break;
            }
        }


        public string[] Serialize() {
            List<string> result = new List<string> {
                "w:" + Width,
                "h:" + Height,
                "fullscreen:" + ( Fullscreen ? "true" : "false" ),
                "forceResizeEnable:" + ( ForceResizeEnable ? "true" : "false" ),
                "mc.aa:" + ( AntiAliasEnabled ? "true" : "false" ),
                "mc.fog:" + ( FogEnabled ? "true" : "false" ),
                "mc.shadow:" + ( ShadowsEnabled ? "true" : "false" ),
                "mc.viewDistance:" + ViewDistance
            };
            result.AddRange( unrecognizedValues.Select( kvp => ( kvp.Key + ':' + kvp.Value ) ) );
            return result.ToArray();
        }
    }
}
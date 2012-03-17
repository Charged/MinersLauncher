using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ChargedMinersLauncher {
    class ChargedMinersSettings {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public bool ForceResizeEnable { get; set; }
        public bool Fullscreen { get; set; }

        static readonly Regex CommentRegex = new Regex( @"^\s*#" );
        static readonly Regex SettingRegex = new Regex( @"^\s*(\S*)\s*=\s*(\S*)" );
        static readonly Regex SettingLiteralRegex = new Regex( @"\s*(\S*)\s*=?\s*:(.*)" );

        public ChargedMinersSettings() {
            Width = 800;
            Height = 600;
            Title = "Charged Miners";
            ForceResizeEnable = false;
            Fullscreen = false;
        }

        public ChargedMinersSettings( IEnumerable<string> lines ) : this() {
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
                case "fullscreen":
                    Fullscreen = Boolean.Parse( value );
                    break;
                case "title":
                    Title = value;
                    break;
                case "forceResizeEnable":
                    ForceResizeEnable = Boolean.Parse( value );
                    break;
            }
        }

        public string[] Serialize() {
            List<string> output = new List<string>();
            output.Add( "w:" + Width );
            output.Add( "h:" + Height );
            output.Add( "fullscreen:" + Fullscreen );
            output.Add( "title:" + Title );
            output.Add( "forceResizeEnable:" + ForceResizeEnable );
            return output.ToArray();
        }
    }
}

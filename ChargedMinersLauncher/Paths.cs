using System;
using System.IO;

namespace ChargedMinersLauncher {
    static class Paths {
        public static string ConfigPath {
            get { return Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" ); }
        }

        public static string ChargeBinary;
        public static string ChargeBinaryAlt;
        public const string ChargeBinaryFormatWindows = "Charge.{0}.exe";
        public const string ChargeBinaryFormatMacOSX = "Charge.{0}.MacOSX";
        public const string ChargeBinaryFormatLinux = "Charge.{0}.Linux";
        public const string ChargeBinaryFormat32Bit = "i386";
        public const string ChargeBinaryFormat64Bit = "x86_64";
        public const string PasswordSaveFile = "saved-login.dat";


        public static bool Init() {
            string tmp;
            if( RuntimeInfo.IsWindows ) {
                tmp = ChargeBinaryFormatWindows;
            } else if( RuntimeInfo.IsMacOSX ) {
                tmp = ChargeBinaryFormatMacOSX;
            } else if( RuntimeInfo.IsLinux ) {
                tmp = ChargeBinaryFormatLinux;
            } else {
                WarningForm.Show( "Unsupported platform", "ChargedMinersLauncher is not supported on this platform." );
                return false;
            }

            if( RuntimeInfo.Is32Bit ) {
                ChargeBinary = String.Format( tmp, ChargeBinaryFormat32Bit );
                ChargeBinaryAlt = "";
            } else if( RuntimeInfo.Is64Bit ) {
                ChargeBinary = String.Format( tmp, ChargeBinaryFormat64Bit );
                ChargeBinaryAlt = String.Format( tmp, ChargeBinaryFormat32Bit );
            }

            // XXX This code should be moved into the auto updater.
            if( !File.Exists( ChargeBinary ) ) {
                // Look for both.
                if( !File.Exists( ChargeBinary ) ) {
                    WarningForm.Show( "Warning", String.Format( "The binary \"{0}\" not found!", ChargeBinary ) );
                } else {
                    ChargeBinary = ChargeBinaryAlt;
                }
            }

            if( !Directory.Exists( ConfigPath ) ) {
                Directory.CreateDirectory( ConfigPath );
            }
            return true;
        }
    }
}

using System;
using System.IO;

namespace ChargedMinersLauncher {
    static class Paths {
        public static string ConfigPath {
            get { return Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" ); }
        }

        public static string PrimaryBinary,
                             AlternativeBinary;
        const string ChargeBinaryFormatWindows = "Charge.{0}.exe";
        const string ChargeBinaryFormatMacOSX = "Charge.{0}.MacOSX";
        const string ChargeBinaryFormatLinux = "Charge.{0}.Linux";
        const string ChargeBinaryFormat32Bit = "i386";
        const string ChargeBinaryFormat64Bit = "x86_64";
        public const string PasswordSaveFile = "saved-login.dat";
        public const string CookieContainerFile = "saved-session.dat";


        public static bool Init() {
            string tmp;
            if( RuntimeInfo.IsWindows ) {
                tmp = ChargeBinaryFormatWindows;
            } else if( RuntimeInfo.IsMacOSX ) {
                tmp = ChargeBinaryFormatMacOSX;
            } else if( RuntimeInfo.IsLinux ) {
                tmp = ChargeBinaryFormatLinux;
            } else {
                return false;
            }

            if( RuntimeInfo.Is32Bit ) {
                PrimaryBinary = String.Format( tmp, ChargeBinaryFormat32Bit );
            } else if( RuntimeInfo.Is64Bit ) {
                PrimaryBinary = String.Format( tmp, ChargeBinaryFormat64Bit );
                AlternativeBinary = String.Format( tmp, ChargeBinaryFormat32Bit );
            } else {
                return false;
            }

            if( !Directory.Exists( ConfigPath ) ) {
                Directory.CreateDirectory( ConfigPath );
            }
            return true;
        }
    }
}

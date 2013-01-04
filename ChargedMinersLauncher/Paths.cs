// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.IO;
using System.Reflection;

namespace ChargedMinersLauncher {
    static class Paths {
        public static readonly string PrimaryBinary,
                                      AlternativeBinary;

        const string ChargeBinaryFormatWindows = "Charge.{0}.exe",
                     ChargeBinaryFormatMacOSX = "Charge.{0}.MacOSX",
                     ChargeBinaryFormatLinux = "Charge.{0}.Linux",
                     ChargeBinarySuffix32Bit = "i386",
                     ChargeBinarySuffix64Bit = "x86_64";

        public const string LegacyPasswordSaveFile = "saved-login.dat",
                            AccountListFile = "accounts.ini",
                            CookieContainerFile = "saved-session.dat";

        public static string LauncherBinaryFile { get; private set; }
        public static string DataDirectory { get; private set; }
        public static string GameSettingsFile { get; private set; }
        public static string GameLogFile { get; private set; }
        public static string LauncherSettingsFile { get; private set; }
        public static string LauncherLogFile { get; private set; }

        public static bool IsPlatformSupported { get; private set; }


        static Paths() {
            LauncherBinaryFile = Assembly.GetExecutingAssembly().Location;
            DataDirectory = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" );
            LauncherSettingsFile = Path.Combine( DataDirectory, "launcher.ini" );
            LauncherLogFile = Path.Combine( DataDirectory, "launcher.log" );
            GameSettingsFile = Path.Combine( DataDirectory, "settings.ini" );
            GameLogFile = Path.Combine( DataDirectory, "log.txt" );

            if( !Directory.Exists( DataDirectory ) ) {
                Directory.CreateDirectory( DataDirectory );
            }
            Directory.SetCurrentDirectory( DataDirectory );

            string tmp;
            if( RuntimeInfo.IsWindows ) {
                tmp = ChargeBinaryFormatWindows;
            } else if( RuntimeInfo.IsMacOSX ) {
                tmp = ChargeBinaryFormatMacOSX;
            } else if( RuntimeInfo.IsLinux ) {
                tmp = ChargeBinaryFormatLinux;
            } else {
                IsPlatformSupported = false;
                return;
            }

            if( RuntimeInfo.Is32Bit ) {
                PrimaryBinary = String.Format( tmp, ChargeBinarySuffix32Bit );
            } else if( RuntimeInfo.Is64Bit ) {
                PrimaryBinary = String.Format( tmp, ChargeBinarySuffix64Bit );
                AlternativeBinary = String.Format( tmp, ChargeBinarySuffix32Bit );
            } else {
                IsPlatformSupported = false;
                return;
            }

            IsPlatformSupported = true;
        }
    }
}
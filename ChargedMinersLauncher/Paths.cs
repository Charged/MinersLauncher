// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.IO;
using System.Reflection;

namespace ChargedMinersLauncher {
    static class Paths {
        public static readonly string PrimaryBinary,
                                      AlternativeBinary;

        const string ChargeBinaryFormatWindows = "Charge.{0}.exe";
        const string ChargeBinaryFormatMacOSX = "Charge.{0}.MacOSX";
        const string ChargeBinaryFormatLinux = "Charge.{0}.Linux";
        const string ChargeBinaryFormat32Bit = "i386";
        const string ChargeBinaryFormat64Bit = "x86_64";
        public const string PasswordSaveFile = "saved-login.dat";
        public const string CookieContainerFile = "saved-session.dat";

        public static string LauncherPath { get; private set; }
        public static string DataPath { get; private set; }
        public static string GameSettingsPath { get; private set; }
        public static string GameLogPath { get; private set; }
        public static string LauncherSettingsPath { get; private set; }
        public static string LauncherLogPath { get; private set; }

        public static bool IsPlatformSupported { get; set; }


        static Paths() {
            LauncherPath = Assembly.GetExecutingAssembly().Location;
            DataPath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" );
            LauncherSettingsPath = Path.Combine( DataPath, "launcher.ini" );
            LauncherLogPath = Path.Combine( DataPath, "launcher.log" );
            GameSettingsPath = Path.Combine( DataPath, "settings.ini" );
            GameLogPath = Path.Combine( DataPath, "log.txt" );

            if( !Directory.Exists( DataPath ) ) {
                Directory.CreateDirectory( DataPath );
            }
            Directory.SetCurrentDirectory( DataPath );

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
                PrimaryBinary = String.Format( tmp, ChargeBinaryFormat32Bit );
            } else if( RuntimeInfo.Is64Bit ) {
                PrimaryBinary = String.Format( tmp, ChargeBinaryFormat64Bit );
                AlternativeBinary = String.Format( tmp, ChargeBinaryFormat32Bit );
            } else {
                IsPlatformSupported = false;
                return;
            }

            IsPlatformSupported = true;
        }
    }
}

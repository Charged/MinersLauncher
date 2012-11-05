﻿// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
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
        public static string ConfigPath { get; private set; }
        public static string LauncherConfigPath { get; private set; }
        public static string SettingsPath { get; private set; }
        public static string LauncherLogPath { get; private set; }

        public static bool IsPlatformSupported { get; set; }


        static Paths() {
            LauncherPath = Assembly.GetExecutingAssembly().Location;
            ConfigPath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" );
            SettingsPath = Path.Combine( ConfigPath, "settings.ini" );
            LauncherConfigPath = Path.Combine( ConfigPath, "launcher.ini" );
            LauncherLogPath = Path.Combine( ConfigPath, "launcher.log" );

            if( !Directory.Exists( ConfigPath ) ) {
                Directory.CreateDirectory( ConfigPath );
            }
            Directory.SetCurrentDirectory( ConfigPath );

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

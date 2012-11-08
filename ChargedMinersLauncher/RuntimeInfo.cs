// Part of ChargedMinersLauncher | Copyright (c) 2012 Jakob Bornecrantz <wallbraker@gmail.com> | Stolen from SirCmpwn | BSD-3 | See LICENSE.txt
using System;
using System.Diagnostics;
using System.IO;

namespace ChargedMinersLauncher {
    static class RuntimeInfo {
        public static bool Is32Bit { get; private set; }
        public static bool Is64Bit { get; private set; }
        public static bool IsMono { get; private set; }
        public static bool IsWindows { get; private set; }
        public static bool IsUnix { get; private set; }
        public static bool IsLinux { get; private set; }
        public static bool IsMacOSX { get; private set; }


        static RuntimeInfo() {
            IsMono = Type.GetType( "Mono.Runtime" ) != null;

            switch( Environment.OSVersion.Platform ) {
                case PlatformID.MacOSX:
                    IsMacOSX = true;
                    IsUnix = true;
                    break;

                case (PlatformID)128:
                case PlatformID.Unix:
                    IsUnix = true;
                    break;

                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    IsWindows = true;
                    break;

                default:
                    return;
            }

            Is32Bit = (IntPtr.Size == 4);
            Is64Bit = (IntPtr.Size == 8);

            if( IsUnix && !IsMacOSX ) {
                Process uname = new Process {
                    StartInfo = {
                        FileName = "uname",
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    }
                };
                uname.Start();
                string output = uname.StandardOutput.ReadToEnd();
                uname.WaitForExit();

                output = output.ToUpper().Replace( "\n", "" ).Trim();

                IsMacOSX = (output == "DARWIN");
                IsLinux = (output == "LINUX");
            }
        }
    }
}
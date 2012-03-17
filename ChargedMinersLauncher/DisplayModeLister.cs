﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ChargedMinersLauncher {
    public class ScreenResolutionLister {
        [DllImport( "user32.dll" )]
        static extern bool EnumDisplaySettings( string deviceName, int modeNum, ref DEVMODE devMode );
        const int ENUM_CURRENT_SETTINGS = -1;
        const int ENUM_REGISTRY_SETTINGS = -2;

        [StructLayout( LayoutKind.Sequential )]
        struct DEVMODE {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs( UnmanagedType.ByValTStr, SizeConst = 0x20 )]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs( UnmanagedType.ByValTStr, SizeConst = 0x20 )]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;

        }

        public static ScreenResolution[] GetList() {
            List<ScreenResolution> resultList = new List<ScreenResolution>();
            DEVMODE vDevMode = new DEVMODE();
            int i = 0;
            while( EnumDisplaySettings( null, i, ref vDevMode ) ) {
                ScreenResolution newRes = new ScreenResolution {
                    Width = vDevMode.dmPelsWidth,
                    Height = vDevMode.dmPelsHeight
                };
                if( !resultList.Contains( newRes ) ) {
                    resultList.Add( newRes );
                }
                i++;
            }
            ScreenResolution[] results = resultList.ToArray();
            Array.Sort( results, CompareResolutions );
            return results;
        }

        public static ScreenResolution GetCurrentResolution() {
            DEVMODE vDevMode = new DEVMODE();
            EnumDisplaySettings( null, ENUM_CURRENT_SETTINGS, ref vDevMode );
            return new ScreenResolution {
                Width = vDevMode.dmPelsWidth,
                Height = vDevMode.dmPelsHeight
            };
        }

        static int CompareResolutions( ScreenResolution a, ScreenResolution b ) {
            if( a.Width == b.Width ) {
                return a.Height - b.Height;
            } else {
                return a.Width - b.Width;
            }
        }
    }

    public struct ScreenResolution : IEquatable<ScreenResolution> {
        public int Width, Height;

        public bool Equals( ScreenResolution other ) {
            return other.Width == Width && other.Height == Height;
        }
        public static bool operator ==( ScreenResolution a, ScreenResolution b ) {
            return a.Equals( b );
        }
        public static bool operator !=( ScreenResolution a, ScreenResolution b ) {
            return !a.Equals( b );
        }
        public override bool Equals( object obj ) {
            if( obj is ScreenResolution ) {
                return Equals( (ScreenResolution)obj );
            } else {
                return base.Equals( obj );
            }
        }
        public override int GetHashCode() {
            return (Width << 16) | Height;
        }
    }
}
using System;

namespace ChargedMinersLauncher {
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
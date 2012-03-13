using System;

namespace ChargedMinersLauncher {
    static class Util {
        public static bool TryParseMiniTimespan( this string text, out TimeSpan result ) {
            try {
                result = ParseMiniTimespan( text );
                return true;
            } catch( ArgumentException ) {
            } catch( OverflowException ) {
            } catch( FormatException ) { }
            result = TimeSpan.Zero;
            return false;
        }


        public static TimeSpan ParseMiniTimespan( this string text ) {
            if( text == null ) throw new ArgumentNullException( "text" );

            text = text.Trim();
            bool expectingDigit = true;
            TimeSpan result = TimeSpan.Zero;
            int digitOffset = 0;
            for( int i = 0; i < text.Length; i++ ) {
                if( expectingDigit ) {
                    if( text[i] < '0' || text[i] > '9' ) {
                        throw new FormatException();
                    }
                    expectingDigit = false;
                } else {
                    if( text[i] >= '0' && text[i] <= '9' ) {
                        continue;
                    } else {
                        string numberString = text.Substring( digitOffset, i - digitOffset );
                        digitOffset = i + 1;
                        int number = Int32.Parse( numberString );
                        switch( Char.ToLower( text[i] ) ) {
                            case 's':
                                result += TimeSpan.FromSeconds( number );
                                break;
                            case 'm':
                                result += TimeSpan.FromMinutes( number );
                                break;
                            case 'h':
                                result += TimeSpan.FromHours( number );
                                break;
                            case 'd':
                                result += TimeSpan.FromDays( number );
                                break;
                            case 'w':
                                result += TimeSpan.FromDays( number * 7 );
                                break;
                            default:
                                throw new FormatException();
                        }
                    }
                }
            }
            return result;
        }
    }
}

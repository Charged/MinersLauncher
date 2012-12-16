// Originally part of fCraft | Copyright (c) 2009-2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace ChargedMinersLauncher {
    /// <summary> Little JSON parsing/serialization library. </summary>
    public sealed class JsonObject : IDictionary<string, object>, ICloneable {
        readonly Dictionary<string, object> data = new Dictionary<string, object>();


        #region Parsing

        int index;
        string str;
        readonly StringBuilder stringParserBuffer = new StringBuilder();
        readonly List<object> arrayParserBuffer = new List<object>();


        /// <summary> Creates an empty JSONObject. </summary>
        public JsonObject() {}


        /// <summary> Creates a JSONObject from a serialized string. </summary>
        /// <param name="inputString"> Serialized JSON object to parse. </param>
        /// <exception cref="ArgumentNullException"> If inputString is null. </exception>
        public JsonObject( string inputString ) {
            if( inputString == null ) throw new ArgumentNullException( "inputString" );
            ReadJsonObject( inputString, 0 );
            Token token = FindNextToken();
            if( token != Token.None ) {
                ThrowUnexpected( token, "None" );
            }
            stringParserBuffer = null;
            arrayParserBuffer = null;
        }


        int ReadJsonObject( string inputString, int offset ) {
            str = inputString;
            index = offset;
            Token token = FindNextToken();
            if( token != Token.BeginObject ) {
                ThrowUnexpected( token, "BeginObject" );
            }

            index++;
            bool first = true;
            do {
                token = FindNextToken();

                if( token == Token.EndObject ) {
                    index++;
                    return index;
                }

                if( first ) {
                    first = false;
                } else if( token == Token.ValueSeparator ) {
                    index++;
                    token = FindNextToken();
                } else {
                    ThrowUnexpected( token, "EndObject or ValueSeparator" );
                }

                if( token != Token.String ) {
                    ThrowUnexpected( token, "String" );
                }

                string key = ReadString();
                token = FindNextToken();
                if( token != Token.NameSeparator ) {
                    ThrowUnexpected( token, "NameSeparator" );
                }
                index++;
                object value = ReadValue();
                Add( key, value );
            } while( token != Token.None );
            return index;
        }


        string ReadString() {
            stringParserBuffer.Length = 0;
            index++;

            for( int start = -1; index < str.Length - 1; index++ ) {
                char c = str[index];

                if( c == '"' ) {
                    if( start != -1 && start != index ) {
                        if( stringParserBuffer.Length == 0 ) {
                            index++;
                            return str.Substring( start, index - start - 1 );
                        } else {
                            stringParserBuffer.Append( str, start, index - start );
                        }
                    }
                    index++;
                    return stringParserBuffer.ToString();
                }

                if( c == '\\' ) {
                    if( start != -1 && start != index ) {
                        stringParserBuffer.Append( str, start, index - start );
                        start = -1;
                    }
                    index++;
                    if( index >= str.Length - 1 ) break;
                    switch( str[index] ) {
                        case '"':
                        case '/':
                        case '\\':
                            start = index;
                            continue;
                        case 'b':
                            stringParserBuffer.Append( '\b' );
                            continue;
                        case 'f':
                            stringParserBuffer.Append( '\f' );
                            continue;
                        case 'n':
                            stringParserBuffer.Append( '\n' );
                            continue;
                        case 'r':
                            stringParserBuffer.Append( '\r' );
                            continue;
                        case 't':
                            stringParserBuffer.Append( '\t' );
                            continue;
                        case 'u':
                            if( index >= str.Length - 5 ) break;
                            uint c0 = ReadHexChar( str[index + 1], 0x1000 );
                            uint c1 = ReadHexChar( str[index + 2], 0x0100 );
                            uint c2 = ReadHexChar( str[index + 3], 0x0010 );
                            uint c3 = ReadHexChar( str[index + 4], 0x0001 );
                            stringParserBuffer.Append( (char)( c0 + c1 + c2 + c3 ) );
                            index += 4;
                            continue;
                    }
                }

                if( c < ' ' ) {
                    ThrowSerialization( "JSONObject: Unexpected character: " +
                                        ( (int)c ).ToString( "X4", NumberFormatInfo.InvariantInfo ) + "." );
                }

                if( start == -1 ) start = index;
            }
            throw new SerializationException( "JSONObject: Unexpected end of string." );
        }


        static uint ReadHexChar( char ch, uint multiplier ) {
            uint val = 0;
            if( ch >= '0' && ch <= '9' ) {
                val = (uint)( ch - '0' ) * multiplier;
            } else if( ch >= 'A' && ch <= 'F' ) {
                val = (uint)( ( ch - 'A' ) + 10 ) * multiplier;
            } else if( ch >= 'a' && ch <= 'f' ) {
                val = (uint)( ( ch - 'a' ) + 10 ) * multiplier;
            } else {
                ThrowSerialization( "JSONObject: Incorrectly specified Unicode entity." );
            }
            return val;
        }


        object ReadValue() {
            switch( FindNextToken() ) {
                case Token.BeginObject:
                    JsonObject newObj = new JsonObject();
                    index = newObj.ReadJsonObject( str, index );
                    return newObj;

                case Token.String:
                    return ReadString();

                case Token.Number:
                    return ReadNumber();

                case Token.Null:
                    if( index >= str.Length - 4 ||
                        str[index + 1] != 'u' || str[index + 2] != 'l' || str[index + 3] != 'l' ) {
                        ThrowSerialization( "JSONObject: Expected 'null'." );
                    }
                    index += 4;
                    return null;

                case Token.True:
                    if( index >= str.Length - 4 ||
                        str[index + 1] != 'r' || str[index + 2] != 'u' || str[index + 3] != 'e' ) {
                        ThrowSerialization( "JSONObject: Expected 'true'." );
                    }
                    index += 4;
                    return true;

                case Token.False:
                    if( index >= str.Length - 5 ||
                        str[index + 1] != 'a' || str[index + 2] != 'l' || str[index + 3] != 's' || str[index + 4] != 'e' ) {
                        ThrowSerialization( "JSONObject: Expected 'false'." );
                    }
                    index += 5;
                    return false;

                case Token.BeginArray:
                    arrayParserBuffer.Clear();
                    index++;
                    bool first = true;
                    while( true ) {
                        Token token = FindNextToken();
                        if( token == Token.EndArray ) break;
                        if( first ) {
                            first = false;
                        } else if( token == Token.ValueSeparator ) {
                            index++;
                        } else {
                            ThrowUnexpected( token, "ValueSeparator" );
                        }
                        arrayParserBuffer.Add( ReadValue() );
                    }
                    index++;
                    return arrayParserBuffer.ToArray();
            }
            throw new SerializationException( "JSONObject: Unexpected token; expected a value." );
        }


        object ReadNumber() {
            long val = 1;
            double doubleVal = Double.NaN;
            bool first = true;
            bool negate = false;

            // Parse sign
            char c = str[index];
            if( str[index] == '-' ) {
                c = str[++index];
                negate = true;
            }

            // Parse integer part
            while( index < str.Length ) {
                if( first ) {
                    if( c == '0' ) {
                        val = 0;
                        c = str[++index];
                        break;
                    } else if( c >= '1' && c <= '9' ) {
                        val = ( c - '0' );
                    }
                } else if( c >= '0' && c <= '9' ) {
                    val *= 10;
                    val += ( c - '0' );
                } else {
                    break;
                }
                first = false;
                c = str[++index];
            }
            if( index >= str.Length ) {
                ThrowSerialization( "JSONObject: Unexpected end of a number (before decimal point)." );
            }

            // Parse fractional part (if present)
            if( c == '.' ) {
                c = str[++index];
                double fraction = 0;
                int multiplier = 10;
                first = true;
                while( index < str.Length ) {
                    if( c >= '0' && c <= '9' ) {
                        fraction += ( c - '0' ) / (double)multiplier;
                        multiplier *= 10;
                    } else if( first ) {
                        ThrowSerialization( "JSONObject: Expected at least one digit after the decimal point." );
                    } else {
                        break;
                    }
                    c = str[++index];
                    first = false;
                }
                if( index >= str.Length ) {
                    ThrowSerialization( "JSONObject: Unexpected end of a number (after decimal point)." );
                }
                doubleVal = val + fraction;
                // Negate (if needed)
                if( negate ) doubleVal = -doubleVal;

            } else {
                if( negate ) val = -val;
            }

            // Parse exponent (if present)
            if( c == 'e' || c == 'E' ) {
                int exponent = 1;
                negate = false;

                // Exponent sign
                c = str[++index];
                switch( c ) {
                    case '-':
                        negate = true;
                        c = str[++index];
                        break;
                    case '+':
                        c = str[++index];
                        break;
                }

                // Exponent value
                first = true;
                while( index < str.Length ) {
                    if( first ) {
                        if( c == '0' ) {
                            exponent = 0;
                            index++;
                            break;
                        } else if( c >= '1' && c <= '9' ) {
                            exponent = ( c - '0' );
                        } else {
                            ThrowSerialization( "JSONObject: Unexpected character in exponent." );
                        }
                    } else if( c >= '0' && c <= '9' ) {
                        exponent *= 10;
                        exponent += ( c - '0' );
                    } else {
                        break;
                    }
                    first = false;
                    c = str[++index];
                }
                if( index >= str.Length ) {
                    ThrowSerialization( "JSONObject: Unexpected end of a number (exponent)." );
                }

                // Apply the exponent
                if( negate ) {
                    exponent = -exponent;
                }
                if( Double.IsNaN( doubleVal ) ) {
                    doubleVal = val;
                }
                doubleVal *= Math.Pow( 10, exponent );
            }

            // Return value in appropriate format
            if( !Double.IsNaN( doubleVal ) ) {
                return doubleVal;
            } else if( val >= Int32.MinValue && val <= Int32.MaxValue ) {
                return (int)val;
            } else {
                return val;
            }
        }


        enum Token {
            None,
            Error,

            BeginObject,
            EndObject,
            BeginArray,
            EndArray,
            NameSeparator,
            ValueSeparator,
            Null,
            True,
            False,
            String,
            Number
        }


        Token FindNextToken() {
            if( index >= str.Length ) return Token.None;
            char c = str[index];
            while( c == ' ' || c == '\t' || c == '\r' || c == '\n' ) {
                index++;
                if( index >= str.Length ) return Token.None;
                c = str[index];
            }
            switch( c ) {
                case '{':
                    return Token.BeginObject;
                case '}':
                    return Token.EndObject;
                case '[':
                    return Token.BeginArray;
                case ']':
                    return Token.EndArray;
                case 'n':
                    return Token.Null;
                case 't':
                    return Token.True;
                case 'f':
                    return Token.False;
                case ':':
                    return Token.NameSeparator;
                case ',':
                    return Token.ValueSeparator;
                case '"':
                    return Token.String;
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return Token.Number;
                default:
                    return Token.Error;
            }
        }


        static void ThrowUnexpected( Token given, string expected ) {
            throw new SerializationException( "JSONObject: Unexpected token " + given + ", expecting " + expected + "." );
        }


        static void ThrowSerialization( string message ) {
            throw new SerializationException( message );
        }

        #endregion


        #region Serialization

        sealed class JsonSerializer {
            readonly StringBuilder sb = new StringBuilder();


            public string Serialize( JsonObject obj ) {
                sb.Length = 0;
                SerializeInternal( obj );
                return sb.ToString();
            }


            void SerializeInternal( JsonObject obj ) {
                sb.Append( '{' );
                if( obj.data.Count > 0 ) {
                    bool first = true;
                    foreach( var kvp in obj.data ) {
                        if( first ) {
                            first = false;
                        } else {
                            sb.Append( ',' );
                        }
                        WriteString( kvp.Key );
                        sb.Append( ':' );
                        WriteValue( kvp.Value );
                    }
                }
                sb.Append( '}' );
            }


            void WriteValue( object obj ) {
                JsonObject asObject;
                string asString;
                Array asArray;
                if( obj == null ) {
                    sb.Append( "null" );
                } else if( ( asObject = obj as JsonObject ) != null ) {
                    SerializeInternal( asObject );
                } else if( obj is int ) {
                    sb.Append( ( (int)obj ).ToString( NumberFormatInfo.InvariantInfo ) );
                } else if( obj is double ) {
                    sb.Append( ( (double)obj ).ToString( NumberFormatInfo.InvariantInfo ) );
                } else if( ( asString = obj as string ) != null ) {
                    WriteString( asString );
                } else if( ( asArray = obj as Array ) != null ) {
                    WriteArray( asArray );
                } else if( obj is long ) {
                    sb.Append( ( (long)obj ).ToString( NumberFormatInfo.InvariantInfo ) );
                } else if( obj is bool ) {
                    if( (bool)obj ) {
                        sb.Append( "true" );
                    } else {
                        sb.Append( "false" );
                    }
                } else {
                    ThrowSerialization( "JSONObject: Non-serializable object found in the collection." );
                }
            }


            void WriteString( string str ) {
                sb.Append( '"' );
                int runIndex = -1;

                for( var i = 0; i < str.Length; i++ ) {
                    var c = str[i];

                    if( c >= ' ' && c < 128 && c != '\"' && c != '\\' ) {
                        if( runIndex == -1 ) {
                            runIndex = i;
                        }
                        continue;
                    }

                    if( runIndex != -1 ) {
                        sb.Append( str, runIndex, i - runIndex );
                        runIndex = -1;
                    }

                    sb.Append( '\\' );
                    switch( c ) {
                        case '\b':
                            sb.Append( 'b' );
                            break;
                        case '\f':
                            sb.Append( 'f' );
                            break;
                        case '\n':
                            sb.Append( 'n' );
                            break;
                        case '\r':
                            sb.Append( 'r' );
                            break;
                        case '\t':
                            sb.Append( 't' );
                            break;
                        case '"':
                        case '\\':
                            sb.Append( c );
                            break;
                        default:
                            sb.Append( 'u' );
                            sb.Append( ( (int)c ).ToString( "X4", NumberFormatInfo.InvariantInfo ) );
                            break;
                    }
                }

                if( runIndex != -1 ) {
                    sb.Append( str, runIndex, str.Length - runIndex );
                }

                sb.Append( '\"' );
            }


            void WriteArray( Array array ) {
                sb.Append( '[' );
                bool first = true;
                for( int i = 0; i < array.Length; i++ ) {
                    if( first ) {
                        first = false;
                    } else {
                        sb.Append( ',' );
                    }
                    WriteValue( array.GetValue( i ) );
                }
                sb.Append( ']' );
            }
        }


        /// <summary> Serializes this JSONObject with default settings. </summary>
        public string Serialize() {
            return new JsonSerializer().Serialize( this );
        }

        #endregion


        #region Has/Get/TryGet shortcuts

        // ==== strings ====
        public string GetString( string key ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            return (string)data[key];
        }

        #endregion


        #region IDictionary / ICollection / ICloneable members

        /// <summary> Creates a JsonObject from an existing JsonObject or string-object dictionary. </summary>
        JsonObject( IEnumerable<KeyValuePair<string, object>> other ) {
            foreach( var kvp in other ) {
                Add( kvp );
            }
        }


        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return data.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() {
            return data.GetEnumerator();
        }


        public void Add( KeyValuePair<string, object> item ) {
            Add( item.Key, item.Value );
        }


        public void Clear() {
            data.Clear();
        }


        bool ICollection<KeyValuePair<string, object>>.Contains( KeyValuePair<string, object> item ) {
            return ( data as ICollection<KeyValuePair<string, object>> ).Contains( item );
        }


        void ICollection<KeyValuePair<string, object>>.CopyTo( KeyValuePair<string, object>[] array, int arrayIndex ) {
            ( data as ICollection<KeyValuePair<string, object>> ).CopyTo( array, arrayIndex );
        }


        bool ICollection<KeyValuePair<string, object>>.Remove( KeyValuePair<string, object> item ) {
            return ( data as ICollection<KeyValuePair<string, object>> ).Remove( item );
        }


        public int Count {
            get { return data.Count; }
        }


        bool ICollection<KeyValuePair<string, object>>.IsReadOnly {
            get { return false; }
        }


        public bool ContainsKey( string key ) {
            return data.ContainsKey( key );
        }


        public void Add( string key, JsonObject obj ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            data.Add( key, obj );
        }


        public void Add( string key, bool obj ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            data.Add( key, obj );
        }


        public void Add( string key, string obj ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            data.Add( key, obj );
        }


        public void Add( string key, object obj ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            if( obj == null || obj is JsonObject || obj is Array ||
                obj is string || obj is int ||
                obj is long || obj is double || obj is bool ) {
                data.Add( key, obj );
            } else if( obj is sbyte ) {
                data.Add( key, (int)(sbyte)obj );
            } else if( obj is byte ) {
                data.Add( key, (int)(byte)obj );
            } else if( obj is short ) {
                data.Add( key, (int)(short)obj );
            } else if( obj is ushort ) {
                data.Add( key, (int)(ushort)obj );
            } else if( obj is uint ) {
                data.Add( key, (long)(uint)obj );
            } else if( obj is float ) {
                data.Add( key, (double)(float)obj );
            } else if( obj is decimal ) {
                data.Add( key, (double)(decimal)obj );
            } else if( obj.GetType().IsEnum ) {
                Add( key, obj.ToString() );
            } else {
                throw new ArgumentException( "JSONObject: Unacceptable value type." );
            }
        }


        public bool Remove( string key ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            return data.Remove( key );
        }


        bool IDictionary<string, object>.TryGetValue( string key, out object value ) {
            if( key == null ) throw new ArgumentNullException( "key" );
            return data.TryGetValue( key, out value );
        }


        public object this[ string key ] {
            get {
                if( key == null ) throw new ArgumentNullException( "key" );
                return data[key];
            }
            set {
                if( key == null ) throw new ArgumentNullException( "key" );
                data[key] = value;
            }
        }


        public ICollection<string> Keys {
            get { return data.Keys; }
        }


        public ICollection<object> Values {
            get { return data.Values; }
        }


        public object Clone() {
            return new JsonObject( this );
        }

        #endregion
    }
}
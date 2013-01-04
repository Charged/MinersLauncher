using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ChargedMinersLauncher {
    /// <summary> Provides simple symmetric AES-256 encryption for passwords. </summary>
    class PasswordSecurity {
        static readonly byte[] KeySalt = {
            0x55, 0x87, 0x14, 0x54,  0x08, 0x9A, 0x40, 0x56,
            0x3C, 0xBD, 0x0A, 0x28,  0xD1, 0xC4, 0x74, 0x79,
            0xAD, 0xD2, 0x79, 0xB9,  0x9A, 0x02, 0xB9, 0xC5,
            0x3F, 0x05, 0x23, 0x41,  0x34, 0x32, 0x6F, 0x97
        };

        static readonly byte[] Key;
        static readonly ushort[] ChecksumTable = new ushort[256];

        static readonly RNGCryptoServiceProvider VectorGenerator = new RNGCryptoServiceProvider();
        static readonly RijndaelManaged Cipher = new RijndaelManaged();

        const int IVSize = 16,
                  KeyBits = 16,
                  ChecksumSize = sizeof( ushort );
        const ushort ChecksumPolynomial = 0xA001;


        static PasswordSecurity() {
            // derive the decryption key from current username (to make password file non-portable)
            Rfc2898DeriveBytes keyGen = new Rfc2898DeriveBytes( Environment.UserName, KeySalt );
            Key = keyGen.GetBytes( KeyBits/sizeof( byte ) );

            InitCrc16();
        }


        // Returns AES-256 encrypted, base-64 encoded password.
        public static string EncryptPassword( string rawPassword ) {
            if( rawPassword == null ) {
                throw new ArgumentNullException( "rawPassword" );
            }
            // generate a random initialization vector
            byte[] vector = new byte[IVSize];
            VectorGenerator.GetBytes( vector );

            // create encryptor with username-derived key and random IV
            ICryptoTransform transform = Cipher.CreateEncryptor( Key, vector );

            MemoryStream stream = new MemoryStream();
            byte[] passwordBytes = Encoding.UTF8.GetBytes( rawPassword );

            // prepend 16-byte initialization vector to the stream
            stream.Write( vector, 0, vector.Length );

            using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
                // compute and prepend CRC-16 checksum to the password
                ushort checksum = ComputeChecksum( passwordBytes, 0, passwordBytes.Length );
                cs.Write( BitConverter.GetBytes( checksum ), 0, ChecksumSize );
                // encrypt password
                cs.Write( passwordBytes, 0, passwordBytes.Length );
            }

            return Convert.ToBase64String( stream.ToArray() );
        }


        // Takes AES-256 encrypted, base-64 encoded password. Returns true if decryption succeeded.
        public static bool DecryptPassword( string encryptedString, out string decryptedPassword ) {
            if( encryptedString == null ) {
                throw new ArgumentNullException( "encryptedString" );
            }
            try {
                byte[] bytesToDecrypt = Convert.FromBase64String( encryptedString );

                // extract the prepended IV, and 
                byte[] vector = new byte[IVSize];
                Buffer.BlockCopy( bytesToDecrypt, 0, vector, 0, IVSize );
                ICryptoTransform transform = Cipher.CreateDecryptor( Key, vector );

                // decrypt the data
                MemoryStream stream = new MemoryStream();
                using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
                    cs.Write( bytesToDecrypt, IVSize, bytesToDecrypt.Length - IVSize );
                }
                byte[] decryptedBytes = stream.ToArray();

                // extract password from decrypted data
                decryptedPassword = Encoding.UTF8.GetString( stream.ToArray(),
                                                             ChecksumSize,
                                                             decryptedBytes.Length - ChecksumSize );

                // calculate expected/actual checksums
                ushort expectedChecksum = BitConverter.ToUInt16( decryptedBytes, 0 );
                ushort actualChecksum = ComputeChecksum( decryptedBytes,
                                                         ChecksumSize,
                                                         decryptedBytes.Length - ChecksumSize );
                if( expectedChecksum == actualChecksum ) {
                    // checksums match, all good
                    return true;
                } else {
                    MainForm.Log( "DecryptPassword: Decryption did not succeed; prefix mismatch." );
                }

            } catch( Exception ex ) {
                MainForm.Log( "DecryptPassword: Error while decrypting: " + ex );
            }
            decryptedPassword = null;
            return false;
        }


        // simple CRC-16 implementation
        static ushort ComputeChecksum( byte[] bytes, int offset, int length ) {
            ushort crc = 0;
            int endIndex = offset + length;
            for( int i = offset; i < endIndex; ++i ) {
                byte index = (byte)( crc ^ bytes[i] );
                crc = (ushort)( ( crc >> 8 ) ^ ChecksumTable[index] );
            }
            return crc;
        }


        static void InitCrc16() {
            for( ushort i = 0; i < ChecksumTable.Length; ++i ) {
                ushort value = 0;
                ushort temp = i;
                for( byte j = 0; j < 8; ++j ) {
                    if( ( ( value ^ temp ) & 0x0001 ) != 0 ) {
                        value = (ushort)( ( value >> 1 ) ^ ChecksumPolynomial );
                    } else {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                ChecksumTable[i] = value;
            }
        }
    }
}
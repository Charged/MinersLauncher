﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ChargedMinersLauncher {
    /// <summary> Provides simple symmetric AES-256 encryption, with a hardcoded key. </summary>
    class PasswordSecurity {
        static readonly byte[] KeySalt = {
            0x55, 0x87, 0x14, 0x54, 0x08, 0x9A, 0x40, 0x56,
            0x3C, 0xBD, 0x0A, 0x28, 0xD1, 0xC4, 0x74, 0x79,
            0xAD, 0xD2, 0x79, 0xB9, 0x9A, 0x02, 0xB9, 0xC5,
            0x3F, 0x05, 0x23, 0x41, 0x34, 0x32, 0x6F, 0x97
        };

        static readonly byte[] Key;

        static readonly RNGCryptoServiceProvider VectorGenerator = new RNGCryptoServiceProvider();
        static readonly RijndaelManaged Cipher = new RijndaelManaged();

        const int IVSize = 16,
                  KeyBits = 16;


        static PasswordSecurity() {
            Rfc2898DeriveBytes keyGen = new Rfc2898DeriveBytes( Environment.UserName, KeySalt );
            Key = keyGen.GetBytes( KeyBits/sizeof( byte ) );
        }


        public static string EncryptPassword( string checkPhrase, string rawPassword ) {
            byte[] vector = new byte[IVSize];
            VectorGenerator.GetBytes( vector );
            ICryptoTransform transform = Cipher.CreateEncryptor( Key, vector );
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes( checkPhrase + rawPassword );

            MemoryStream stream = new MemoryStream();
            stream.Write( vector, 0, vector.Length );
            using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
                cs.Write( bytesToEncrypt, 0, bytesToEncrypt.Length );
            }

            return Convert.ToBase64String( stream.ToArray() );
        }


        public static bool DecryptPassword( string checkPhrase, string encryptedString, out string decryptedPassword ) {
            try {
                byte[] bytesToDecrypt = Convert.FromBase64String( encryptedString );
                byte[] vector = new byte[IVSize];
                Buffer.BlockCopy( bytesToDecrypt, 0, vector, 0, IVSize );
                ICryptoTransform transform = Cipher.CreateDecryptor( Key, vector );

                MemoryStream stream = new MemoryStream();
                using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
                    cs.Write( bytesToDecrypt, IVSize, bytesToDecrypt.Length - IVSize );
                }
                string result = Encoding.ASCII.GetString( stream.ToArray() );
                if( result.StartsWith( checkPhrase ) ) {
                    decryptedPassword = result.Substring( checkPhrase.Length );
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
    }
}
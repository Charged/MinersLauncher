using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SimplerAES {
    private static readonly byte[] Key = { 0x55, 0x87, 0x14, 0x54, 0x08, 0x9A, 0x40, 0x56,
                                           0x3C, 0xBD, 0x0A, 0x28, 0xD1, 0xC4, 0x74, 0x79,
                                           0xAD, 0xD2, 0x79, 0xB9, 0x9A, 0x02, 0xB9, 0xC5,
                                           0x3F, 0x05, 0x23, 0x41, 0x34, 0x32, 0x6F, 0x97,
                                           0x24, 0xFF, 0xCD, 0x5F, 0x9D, 0x45, 0x01, 0x64,
                                           0x10, 0xAE, 0x66, 0x5D, 0x5B, 0x3A, 0x84, 0x63,
                                           0x17, 0xCA, 0xDD, 0xAA, 0x08, 0x02, 0x87, 0xBE,
                                           0x9D, 0xCB, 0xAE, 0x83, 0xA8, 0x62, 0xAD, 0xA6,
                                         
                                           0xBD, 0xBA, 0x05, 0x95, 0xDD, 0x83, 0x59, 0xC8,
                                           0xC0, 0xD9, 0x82, 0xCC, 0x3A, 0x40, 0xB3, 0x63,
                                           0x93, 0xAF, 0xD4, 0x3C, 0x77, 0x2E, 0xC5, 0x10,
                                           0xF7, 0x8C, 0x0E, 0x32, 0xD6, 0x58, 0xA7, 0x5E,
                                           0x34, 0xFC, 0xFE, 0x74, 0x0F, 0x70, 0x1A, 0xE3,
                                           0xBE, 0x7E, 0xF6, 0x1F, 0xE2, 0x54, 0x9F, 0x31,
                                           0x2C, 0xDD, 0xCB, 0xEF, 0xD0, 0x8A, 0x15, 0x05,
                                           0x8F, 0x04, 0x19, 0x57, 0xF5, 0x1C, 0xE9, 0x35 };

    static readonly RNGCryptoServiceProvider VectorGenerator = new RNGCryptoServiceProvider();
    static readonly RijndaelManaged Cipher = new RijndaelManaged();
    const int IVSize = 16;


    public static string EncryptPassword( string rawPassword ) {
        byte[] vector = new byte[IVSize];
        VectorGenerator.GetBytes( vector );
        ICryptoTransform transform = Cipher.CreateEncryptor( Key, vector );
        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes( rawPassword );

        MemoryStream stream = new MemoryStream();
        stream.Write( vector, 0, vector.Length );
        using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
            cs.Write( bytesToEncrypt, 0, bytesToEncrypt.Length );
        }

        return Convert.ToBase64String( stream.ToArray() );
    }


    public static string DecryptPassword( string encryptedPassword ) {
        byte[] bytesToDecrypt = Convert.FromBase64String( encryptedPassword );
        byte[] vector = new byte[IVSize];
        Buffer.BlockCopy( bytesToDecrypt, 0, vector, 0, IVSize );
        ICryptoTransform transform = Cipher.CreateDecryptor( Key, vector );

        MemoryStream stream = new MemoryStream();
        using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
            cs.Write( bytesToDecrypt, IVSize, bytesToDecrypt.Length );
        }
        return Encoding.ASCII.GetString( stream.ToArray() );
    }


    static byte[] Transform( byte[] prefix, byte[] buffer, ICryptoTransform transform ) {
        MemoryStream stream = new MemoryStream();
        stream.Write( prefix, 0, prefix.Length );
        using( CryptoStream cs = new CryptoStream( stream, transform, CryptoStreamMode.Write ) ) {
            cs.Write( buffer, 0, buffer.Length );
        }
        return stream.ToArray();
    }
}
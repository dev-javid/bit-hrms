using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.EntityFramework.Interceptors
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("o977Kqi5zmLREhR75blkyEg4y3zPk3WCndl5UBp6bdXbgfsS3Ao3zKrvv6J0t4NX"); // Key should be 16 bytes for AES-128, 24 bytes for AES-192, and 32 bytes for AES-256
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("LWHiT2wqknh3g6jFyaxGsg=="); // IV should be 16 bytes

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }

            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return cipherText;
            }

            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}

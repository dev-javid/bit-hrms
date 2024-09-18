using System.Security.Cryptography;

namespace Infrastructure.EntityFramework.Encryption
{
    public interface IEncryptionProvider
    {
        byte[] Encrypt(byte[] input);

        byte[] Decrypt(byte[] input);
    }

    public class AesEncryptionProvider : IEncryptionProvider
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public AesEncryptionProvider(byte[] key, byte[] iv)
        {
            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException("Key size must be 128, 192, or 256 bits.");
            }

            if (iv.Length != 16)
            {
                throw new ArgumentException("IV size must be 128 bits.");
            }

            this.key = key;
            this.iv = iv;
        }

        public byte[] Encrypt(byte[] input)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        public byte[] Decrypt(byte[] input)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(input);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var resultStream = new MemoryStream();
            cryptoStream.CopyTo(resultStream);
            return resultStream.ToArray();
        }
    }
}

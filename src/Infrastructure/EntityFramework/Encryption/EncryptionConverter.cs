using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Infrastructure.EntityFramework.Encryption.PropertyBuilderExtensions;

namespace Infrastructure.EntityFramework.Encryption
{
    internal sealed class EncryptionConverter<TModel, TProvider> : ValueConverter<TModel, TProvider>
    {
        public EncryptionConverter(IEncryptionProvider encryptionProvider, StorageFormat storageFormat)
            : base(
                x => Encrypt<TModel, TProvider>(x, encryptionProvider, storageFormat),
                x => Decrypt(x, encryptionProvider, storageFormat),
                null)
        {
        }

        private static TOutput Encrypt<TInput, TOutput>(TInput input, IEncryptionProvider encryptionProvider, StorageFormat storageFormat)
        {
            var inputData = input switch
            {
                string => !string.IsNullOrEmpty(input.ToString()) ? Encoding.UTF8.GetBytes(input.ToString()!) : null,
                byte[] => input as byte[],
                _ => null,
            };

            byte[] encryptedRawBytes = encryptionProvider.Encrypt(inputData!);

            if (encryptedRawBytes is null)
            {
                return default!;
            }

            object encryptedData = storageFormat switch
            {
                StorageFormat.Default or StorageFormat.Base64 => Convert.ToBase64String(encryptedRawBytes),
                _ => encryptedRawBytes
            };

            return (TOutput)Convert.ChangeType(encryptedData, typeof(TOutput));
        }

        private static TModel Decrypt(TProvider input, IEncryptionProvider encryptionProvider, StorageFormat storageFormat)
        {
            Type destinationType = typeof(TModel);
            var inputData = storageFormat switch
            {
                StorageFormat.Default or StorageFormat.Base64 => Convert.FromBase64String(input!.ToString()!),
                _ => input as byte[]
            };

            byte[] decryptedRawBytes = encryptionProvider.Decrypt(inputData!);
            object decryptedData = null!;

            if (decryptedRawBytes != null && destinationType == typeof(string))
            {
                decryptedData = Encoding.UTF8.GetString(decryptedRawBytes).Trim('\0');
            }
            else if (destinationType == typeof(byte[]))
            {
                decryptedData = decryptedRawBytes!;
            }

            return (TModel)Convert.ChangeType(decryptedData, typeof(TModel));
        }
    }
}

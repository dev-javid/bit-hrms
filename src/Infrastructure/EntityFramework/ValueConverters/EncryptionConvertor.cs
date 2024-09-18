using Infrastructure.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.ValueConverters
{
    public class EncryptionConvertor() :
        ValueConverter<string, string>(
            x => EncryptionHelper.Encrypt(x),
            x => EncryptionHelper.Decrypt(x),
            null)
    {
    }
}

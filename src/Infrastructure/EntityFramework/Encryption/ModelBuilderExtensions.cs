using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Infrastructure.EntityFramework.Encryption.PropertyBuilderExtensions;

namespace Infrastructure.EntityFramework.Encryption
{
    internal static class ModelBuilderExtensions
    {
        public static ModelBuilder UseEncryption(this ModelBuilder modelBuilder, IEncryptionProvider encryptionProvider)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            if (encryptionProvider is null)
            {
                throw new ArgumentNullException(nameof(encryptionProvider));
            }

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                IEnumerable<EncryptedProperty> encryptedProperties = GetEntityEncryptedProperties(entityType);

                foreach (EncryptedProperty encryptedProperty in encryptedProperties)
                {
#pragma warning disable EF1001 // Internal EF Core API usage.
                    if (encryptedProperty.Property.FindAnnotation(CoreAnnotationNames.ValueConverter) is not null)
                    {
                        continue;
                    }
#pragma warning restore EF1001 // Internal EF Core API usage.

                    ValueConverter converter = GetValueConverter(encryptedProperty.Property.ClrType, encryptionProvider, encryptedProperty.StorageFormat);

                    if (converter != null)
                    {
                        encryptedProperty.Property.SetValueConverter(converter);
                    }
                }
            }

            return modelBuilder;
        }

        private static ValueConverter GetValueConverter(Type propertyType, IEncryptionProvider encryptionProvider, StorageFormat storageFormat)
        {
            if (propertyType == typeof(string))
            {
                return storageFormat switch
                {
                    StorageFormat.Default or StorageFormat.Base64 => new EncryptionConverter<string, string>(encryptionProvider, StorageFormat.Base64),
                    StorageFormat.Binary => new EncryptionConverter<string, byte[]>(encryptionProvider, StorageFormat.Binary),
                    _ => throw new NotImplementedException()
                };
            }
            else if (propertyType == typeof(byte[]))
            {
                return storageFormat switch
                {
                    StorageFormat.Default or StorageFormat.Binary => new EncryptionConverter<byte[], byte[]>(encryptionProvider, StorageFormat.Binary),
                    StorageFormat.Base64 => new EncryptionConverter<byte[], string>(encryptionProvider, StorageFormat.Base64),
                    _ => throw new NotImplementedException()
                };
            }

            throw new NotImplementedException($"Type {propertyType.Name} does not support encryption.");
        }

        private static IEnumerable<EncryptedProperty> GetEntityEncryptedProperties(IMutableEntityType entity)
        {
            if (entity.Name.Contains("Compensation"))
            {
                foreach (var item in entity.GetProperties())
                {
                    EncryptedProperty.Create(item);
                }
            }

            return entity.GetProperties()
                .Select(x => EncryptedProperty.Create(x)!)
                .Where(x => x != null);
        }

        internal class EncryptedProperty
        {
            private EncryptedProperty(IMutableProperty property, StorageFormat storageFormat)
            {
                Property = property;
                StorageFormat = storageFormat;
            }

            public IMutableProperty Property { get; }

            public StorageFormat StorageFormat { get; }

            public static EncryptedProperty? Create(IMutableProperty property)
            {
                StorageFormat? storageFormat = null;

                var encryptedAnnotation = property.FindAnnotation(PropertyAnnotations.IsEncrypted);

                if (encryptedAnnotation?.Value != null && (bool)encryptedAnnotation.Value)
                {
                    storageFormat = (StorageFormat)property.FindAnnotation(PropertyAnnotations.StorageFormat)?.Value!;
                }

                return storageFormat.HasValue ? new EncryptedProperty(property, storageFormat.Value) : null;
            }
        }
    }
}

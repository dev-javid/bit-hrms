namespace Infrastructure.EntityFramework.Encryption
{
    public static class PropertyBuilderExtensions
    {
        public enum StorageFormat
        {
            Default,
            Binary,
            Base64,
        }

        public static PropertyBuilder<TProperty> IsEncrypted<TProperty>(this PropertyBuilder<TProperty> builder, StorageFormat storageFormat = StorageFormat.Default)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasAnnotation(PropertyAnnotations.IsEncrypted, true);
            builder.HasAnnotation(PropertyAnnotations.StorageFormat, storageFormat);

            return builder;
        }
    }
}

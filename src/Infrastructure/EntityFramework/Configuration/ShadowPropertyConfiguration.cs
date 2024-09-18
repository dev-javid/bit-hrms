namespace Infrastructure.EntityFramework.Configuration
{
    public class ShadowPropertyConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<int>("CreatedBy")
                .HasColumnName("created_by");

            builder.Property<DateTime>("CreatedOn")
                .HasColumnName("created_on");

            builder.Property<int?>("ModifiedBy")
                .IsRequired(false)
                .HasColumnName("modified_by");

            builder.Property<DateTime?>("ModifiedOn")
                .IsRequired(false)
                .HasColumnName("modified_on");
        }
    }
}

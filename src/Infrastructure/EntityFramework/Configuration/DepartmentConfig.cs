using Domain.Companies;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");

            builder
                .Property(x => x.Name)
                .HasColumnName("name");
        }
    }
}

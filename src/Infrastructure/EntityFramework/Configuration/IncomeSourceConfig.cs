using Domain.Finance;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class IncomeSourceConfig : IEntityTypeConfiguration<IncomeSource>
    {
        public void Configure(EntityTypeBuilder<IncomeSource> builder)
        {
            builder.ToTable("income_sources");

            builder
                .Property(x => x.Name)
                .HasColumnName("name");

            builder
                .Property(x => x.Description)
                .HasColumnName("description");
        }
    }
}

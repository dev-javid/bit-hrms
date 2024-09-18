using Domain.Companies;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class HolidayConfig : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("holidays");

            builder
                .Property(x => x.Name)
                .HasColumnName("name");

            builder
                .Property(x => x.Date)
                .HasColumnName("date");

            builder
                .Property(x => x.Optional)
                .HasColumnName("optional");
        }
    }
}

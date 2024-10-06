using Domain.Companies;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder
                .Property(x => x.OwnerUserId)
                .HasColumnName("owner_user_id");

            builder
                .Property(x => x.Name)
                .HasColumnName("name");

            builder
                .Property(x => x.AdministratorName)
                .HasColumnName("administrator_name");

            builder
                .ComplexProperty(x => x.Email)
                .Property(x => x.Value)
                .HasColumnName("email");

            builder
                .ComplexProperty(x => x.PhoneNumber)
                .Property(x => x.Value)
                .HasColumnName("phone_number");

            builder
                .ComplexProperty(x => x.FinancialMonth)
                .Property(x => x.Value)
                .HasColumnName("financial_month");

            builder
                .Property(x => x.Address)
                .HasColumnName("address");

            builder
                .Property(x => x.IsDeleted)
                .HasColumnName("is_deleted");

            builder
                .Property(x => x.WeeklyOffDays)
                .HasColumnName("weekly_off_days")
                .HasConversion(
                    new ValueConverter<DayOfWeek[], string[]>(
                        v => v.Select(x => x.ToString()).ToArray(),
                        v => v.Select(x => Enum.Parse<DayOfWeek>(x)).ToArray()));
        }
    }
}

using Domain.Companies;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

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
        }
    }
}

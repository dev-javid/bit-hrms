using Domain.Salaries;
using Infrastructure.EntityFramework.Encryption;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class SalaryConfig : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.ToTable("salaries");

            builder
                .Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            builder
                .ComplexProperty(x => x.Month)
                .Property(x => x.Value)
                .HasColumnName("month");

            builder
                .Property(x => x.Year)
                .HasColumnName("year");

            builder
                .Property(x => x.AmountEncrypted)
                .IsEncrypted()
                .HasColumnName("amount");
        }
    }
}

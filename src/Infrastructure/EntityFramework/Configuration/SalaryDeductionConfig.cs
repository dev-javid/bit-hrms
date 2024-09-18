using Domain.Salaries;
using Infrastructure.EntityFramework.Encryption;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class SalaryDeductionConfig : IEntityTypeConfiguration<SalaryDeduction>
    {
        public void Configure(EntityTypeBuilder<SalaryDeduction> builder)
        {
            builder.ToTable("salary_deductions");

            builder
                .Property(x => x.SalaryId)
                .HasColumnName("salary_id");

            builder
                .Property(x => x.DeductionType)
                .HasColumnName("deduction_type")
                .HasConversion(
                     (x) => x.ToString(),
                     y => Enum.Parse<DeductionType>(y));

            builder
                .Property(x => x.AmountEncrypted)
                .IsEncrypted()
                .HasColumnName("amount");
        }
    }
}

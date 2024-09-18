using Domain.Employees;
using Infrastructure.EntityFramework.Encryption;

namespace Infrastructure.EntityFramework.Configuration
{
    public class CompensationConfig : IEntityTypeConfiguration<Compensation>
    {
        public void Configure(EntityTypeBuilder<Compensation> builder)
        {
            builder.ToTable("compensations");

            builder
                .Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            builder
                .Property(x => x.EffectiveFrom)
                .HasColumnName("effective_from");

            builder
                .Property(x => x.AmountEncrypted)
                .IsEncrypted()
                .HasColumnName("amount");
        }
    }
}

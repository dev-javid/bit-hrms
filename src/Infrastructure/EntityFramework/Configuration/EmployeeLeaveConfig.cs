using Domain.Employees;
using Infrastructure.EntityFramework.ValueConverters;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class EmployeeLeaveConfig : IEntityTypeConfiguration<EmployeeLeave>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeave> builder)
        {
            builder.ToTable("employee_leaves");

            builder
                .Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            builder
                .Property(x => x.From)
                .HasColumnName("from_date");

            builder
                .Property(x => x.To)
                .HasColumnName("to_date");

            builder
                .Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion(
                    x => x.ToString(),
                    x => Enum.Parse<EmployeeLeave.LeaveStatus>(x));

            builder
                .Property(x => x.Remarks)
                .HasColumnName("remarks");
        }
    }
}

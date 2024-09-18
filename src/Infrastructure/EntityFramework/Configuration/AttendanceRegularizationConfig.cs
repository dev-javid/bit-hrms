using Domain.Attendance;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class AttendanceRegularizationConfig : IEntityTypeConfiguration<AttendanceRegularization>
    {
        public void Configure(EntityTypeBuilder<AttendanceRegularization> builder)
        {
            builder.ToTable("attendance_regularization");

            builder
                .Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            builder
                .Property(x => x.Date)
                .HasColumnName("date");

            builder
                .Property(x => x.ClockInTime)
                .HasColumnName("clock_in_time");

            builder
                .Property(x => x.ClockOutTime)
                .HasColumnName("clock_out_time");

            builder
                .Property(x => x.Reason)
                .HasColumnName("reason");

            builder
                .Property(x => x.Approved)
                .HasColumnName("approved");
        }
    }
}

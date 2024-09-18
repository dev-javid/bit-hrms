using Domain.Attendance;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class ClockInOutTimingConfig : IEntityTypeConfiguration<ClockInOutTiming>
    {
        public void Configure(EntityTypeBuilder<ClockInOutTiming> builder)
        {
            builder.ToTable("clock_in_out_times");

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
        }
    }
}

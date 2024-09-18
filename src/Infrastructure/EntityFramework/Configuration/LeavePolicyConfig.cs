using Domain.Companies;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class LeavePolicyConfig : IEntityTypeConfiguration<LeavePolicy>
    {
        public void Configure(EntityTypeBuilder<LeavePolicy> builder)
        {
            builder.ToTable("leave_policies");

            builder
                .Property(x => x.CasualLeaves)
                .HasColumnName("casual_leaves");

            builder
                .Property(x => x.EarnedLeavesPerMonth)
                .HasColumnName("earned_leaves_per_month");

            builder
                .Property(x => x.Holidays)
                .HasColumnName("holidays");
        }
    }
}

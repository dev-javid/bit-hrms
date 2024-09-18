using Domain.Companies;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class JobTitleConfig : IEntityTypeConfiguration<JobTitle>
    {
        public void Configure(EntityTypeBuilder<JobTitle> builder)
        {
            builder.ToTable("job_titles");

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.DepartmentId)
                .HasColumnName("department_id");

            builder
                .Property(x => x.Name)
                .HasColumnName("name");
        }
    }
}

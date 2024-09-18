using Domain.Employees;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .Property(x => x.DepartmentId)
                .HasColumnName("department_id");

            builder
                .Property(x => x.JobTitleId)
                .HasColumnName("job_title_id");

            builder
                .Property(x => x.FirstName)
                .HasColumnName("first_name");

            builder
                .Property(x => x.LastName)
                .HasColumnName("last_name");

            builder
                .Property(x => x.DateOfBirth)
                .HasColumnName("date_of_birth");

            builder
                .Property(x => x.DateOfJoining)
                .HasColumnName("date_of_joining");

            builder
                .Property(x => x.FatherName)
                .HasColumnName("father_name");

            builder
                .ComplexProperty(x => x.PhoneNumber)
                .Property(x => x.Value)
                .HasColumnName("phone_number");

            builder
                .ComplexProperty(x => x.PersonalEmail)
                .Property(x => x.Value)
                .HasColumnName("personal_email");

            builder
                .ComplexProperty(x => x.CompanyEmail)
                .Property(x => x.Value)
                .HasColumnName("company_email");

            builder
                .Property(x => x.Address)
                .HasColumnName("address");

            builder
                .Property(x => x.City)
                .HasColumnName("city");

            builder
                .ComplexProperty(x => x.PAN)
                .Property(x => x.Value)
                .HasColumnName("pan");

            builder
                .ComplexProperty(x => x.Aadhar)
                .Property(x => x.Value)
                .HasColumnName("aadhar");
        }
    }
}

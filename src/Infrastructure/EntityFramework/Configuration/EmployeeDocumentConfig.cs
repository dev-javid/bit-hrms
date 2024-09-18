using Domain.Employees;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class EmployeeDocumentConfig : IEntityTypeConfiguration<EmployeeDocument>
    {
        public void Configure(EntityTypeBuilder<EmployeeDocument> builder)
        {
            builder.ToTable("employee_documents");

            builder
                .Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            builder
                .Property(x => x.DocumentType)
                .HasColumnName("document_type")
                .HasConversion(
                    x => x.ToString(),
                    x => Enum.Parse<DocumentType>(x));

            builder
                .ComplexProperty(x => x.FileName)
                .Property(x => x.Value)
                .HasColumnName("file_name");
        }
    }
}

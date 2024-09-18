namespace Domain.Employees
{
    public class EmployeeDocument : CompanyEntity<int>
    {
        private EmployeeDocument()
        {
        }

        public int EmployeeId { get; private set; }

        public DocumentType DocumentType { get; private set; }

        public FileName FileName { get; private set; } = null!;

        internal static EmployeeDocument Create(FileName fileName, DocumentType documentType)
        {
            return new EmployeeDocument
            {
                DocumentType = documentType,
                FileName = fileName,
            };
        }

        internal void Update(FileName fileName)
        {
            FileName = fileName;
        }
    }
}

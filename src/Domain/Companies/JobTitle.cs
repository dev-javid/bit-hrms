namespace Domain.Companies
{
    public class JobTitle : CompanyEntity<int>
    {
        private JobTitle()
        {
        }

        public string Name { get; private set; } = null!;

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        internal static JobTitle Create(string name)
        {
            return new JobTitle
            {
                Name = name,
            };
        }

        internal void SetName(string name)
        {
            Name = name;
        }
    }
}

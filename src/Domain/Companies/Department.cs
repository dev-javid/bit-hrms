namespace Domain.Companies
{
    public class Department : CompanyEntity<int>
    {
        private Department()
        {
        }

        public string Name { get; private set; } = null!;

        public ICollection<JobTitle> JobTitles { get; private set; } = [];

        internal static Department Create(string name)
        {
            return new Department
            {
                Name = name,
            };
        }

        internal void SetName(string name)
        {
            Name = name;
        }

        internal JobTitle AddJobTitle(string name)
        {
            if (JobTitles.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Job title with same name already exists");
            }

            var jobTitle = JobTitle.Create(name);
            JobTitles.Add(jobTitle);
            return jobTitle;
        }

        internal JobTitle UpdateJobTitle(int jobTitleId, string name)
        {
            var jobTitle = JobTitles.FirstOrDefault(x => x.Id == jobTitleId) ?? throw CustomException.WithNotFound("Job title not found");

            if (JobTitles.Any(x => x.Id != jobTitleId && x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Another job title with same name already exists");
            }

            jobTitle.SetName(name);
            return jobTitle;
        }
    }
}

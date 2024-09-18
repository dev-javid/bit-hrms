namespace Domain.Companies
{
    public class Holiday : CompanyEntity<int>
    {
        private Holiday()
        {
        }

        public string Name { get; private set; } = null!;

        public DateOnly Date { get; private set; }

        public bool Optional { get; private set; }

        internal static Holiday Create(string name, DateOnly date, bool optional)
        {
            return new Holiday
            {
                Name = name,
                Date = date,
                Optional = optional
            };
        }

        internal void Update(string name, DateOnly date, bool optional)
        {
            Name = name;
            Date = date;
            Optional = optional;
        }
    }
}

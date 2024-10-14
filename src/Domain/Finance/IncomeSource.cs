namespace Domain.Finance
{
    public class IncomeSource : CompanyEntity<int>
    {
        private IncomeSource()
        {
            Incomes = [];
        }

        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public ICollection<Income> Incomes { get; set; }

        public void AddIncome(decimal amount, IEnumerable<FileName> documents, string? remarks)
        {
            Incomes.Add(Income.Create(amount, documents, remarks));
        }

        internal static IncomeSource Create(string name, string description)
        {
            return new IncomeSource()
            {
                Name = name,
                Description = description
            };
        }

        internal void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

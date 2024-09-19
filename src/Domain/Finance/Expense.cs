namespace Domain.Finance
{
    public class Expense : CompanyEntity<int>
    {
        private Expense()
        {
            Documents = [];
        }

        public decimal Amount => AmountEncrypted.ToDecimal();

        public byte[] AmountEncrypted { get; private set; } = null!;

        public string? Purpose { get; set; }

        public FileName[] Documents { get; set; }

        internal static Expense Create(decimal amount, string? purpose, IEnumerable<FileName> documents)
        {
            return new Expense
            {
                AmountEncrypted = amount.ToBytes(),
                Documents = documents.ToArray(),
                Purpose = purpose
            };
        }
    }
}

namespace Domain.Finance
{
    public class Expanse : CompanyEntity<int>
    {
        private Expanse()
        {
            Documents = [];
        }

        public decimal Amount => AmountEncrypted.ToDecimal();

        public byte[] AmountEncrypted { get; private set; } = null!;

        public string? Purpose { get; set; }

        public FileName[] Documents { get; set; }

        internal static Expanse Create(decimal amount, string? purpose, IEnumerable<FileName> documents)
        {
            return new Expanse
            {
                AmountEncrypted = amount.ToBytes(),
                Documents = documents.ToArray(),
                Purpose = purpose
            };
        }
    }
}

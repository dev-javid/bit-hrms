namespace Domain.Finance
{
    public class Income : CompanyEntity<int>
    {
        private Income()
        {
            Documents = [];
        }

        public int IncomeSourceId { get; private set; }

        public decimal Amount => AmountEncrypted.ToDecimal();

        public byte[] AmountEncrypted { get; private set; } = null!;

        public string? Remarks { get; set; }

        public FileName[] Documents { get; set; }

        public IncomeSource IncomeSource { get; set; } = null!;

        internal static Income Create(decimal amount, IEnumerable<FileName> documents, string? remarks)
        {
            return new Income
            {
                AmountEncrypted = amount.ToBytes(),
                Documents = documents.ToArray(),
                Remarks = remarks
            };
        }
    }
}

namespace Domain.Employees
{
    public class Compensation : CompanyEntity<int>
    {
        private Compensation()
        {
        }

        public int EmployeeId { get; private set; }

        public DateOnly EffectiveFrom { get; private set; }

        public decimal Amount => AmountEncrypted.ToDecimal();

        public byte[] AmountEncrypted { get; private set; } = null!;

        public Employee Employee { get; set; } = null!;

        internal static Compensation Create(int employeeId, DateOnly effectiveFrom, decimal amount)
        {
            return new Compensation
            {
                EmployeeId = employeeId,
                EffectiveFrom = effectiveFrom,
                AmountEncrypted = amount.ToBytes()
            };
        }
    }
}

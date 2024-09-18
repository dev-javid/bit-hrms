namespace Domain.Salaries;

public class SalaryDeduction : CompanyEntity<int>
{
    private SalaryDeduction()
    {
    }

    public int SalaryId { get; private set; }

    public decimal Amount => AmountEncrypted.ToDecimal();

    public byte[] AmountEncrypted { get; private set; } = null!;

    public DeductionType DeductionType { get; private set; }

    internal static SalaryDeduction Create(decimal amount, DeductionType deductionType)
    {
        return new SalaryDeduction()
        {
            AmountEncrypted = amount.ToBytes(),
            DeductionType = deductionType
        };
    }
}

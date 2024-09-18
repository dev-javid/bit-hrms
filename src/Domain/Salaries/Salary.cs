using Domain.Employees;

namespace Domain.Salaries;

public class Salary : CompanyEntity<int>
{
    private Salary()
    {
        SalaryDudections = [];
    }

    public int EmployeeId { get; private set; }

    public Month Month { get; private set; } = null!;

    public int Year { get; private set; }

    public decimal Amount => AmountEncrypted.ToDecimal();

    public byte[] AmountEncrypted { get; private set; } = null!;

    public Employee Employee { get; set; } = null!;

    public ICollection<SalaryDeduction> SalaryDudections { get; set; }

    public static Salary Create(int employeeId, Month month, int year, decimal netAmount)
    {
        return new Salary
        {
            EmployeeId = employeeId,
            Month = month,
            AmountEncrypted = netAmount.ToBytes(),
            Year = year,
        };
    }

    public void AddDeduction(DeductionType deductionType, decimal amount)
    {
        SalaryDudections.Add(SalaryDeduction.Create(amount, deductionType));
    }
}

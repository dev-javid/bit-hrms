namespace Application.Common.Abstract;

public interface ICurrentUser
{
    int Id { get; }

    int EmployeeId { get; }

    int CompanyId { get; }

    bool IsSuperAdmin { get; }

    bool IsEmployee { get; }

    bool IsCompanyAdmin { get; }
}

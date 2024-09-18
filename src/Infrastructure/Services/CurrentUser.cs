using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;
    private readonly bool _isAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public int Id => _isAuthenticated && _user != null ? int.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0") : -1;

    public int CompanyId => _isAuthenticated && _user != null ? int.Parse(_user.FindFirstValue("companyId") ?? "0") : 0;

    public int EmployeeId => _isAuthenticated && _user != null ? int.Parse(_user.FindFirstValue("employeeId") ?? "0") : 0;

    public bool IsSuperAdmin => _isAuthenticated && _user != null && _user.IsInRole(RoleName.SuperAdmin.ToString());

    public bool IsCompanyAdmin => _isAuthenticated && _user != null && _user.IsInRole(RoleName.CompanyAdmin.ToString());

    public bool IsEmployee => _isAuthenticated && _user != null && _user.IsInRole(RoleName.Employee.ToString());
}

namespace Presentation.DependencyRegistration
{
    public static class AuthPolicy
    {
        public const string AllRoles = "ALL-ROLES-CAN-ACCESS";
        public const string SuperAdmin = "ONLY-SUPER-ADMIN-CAN-ACCESS";
        public const string CompanyAdmin = "ONLY-COMPANY-ADMIN-CAN-ACCESS";
        public const string CompanyAdminOrEmployee = "COMPANY-ADMIN-AND-EMPLOYEE-CAN-ACCESS";
        public const string Employee = "ONLY-EMPLOYEE-CAN-ACCESS";
        public const string AllowAnonymous = "ALLOW-ANONYMOUS";
    }
}

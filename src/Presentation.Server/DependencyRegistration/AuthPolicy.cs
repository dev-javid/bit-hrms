using static Domain.Common.Enums;

namespace Presentation.DependencyRegistration
{
    internal static class AuthPolicy
    {
        public const string AllRoles = "ALL-ROLES-CAN-ACCESS";
        public const string SuperAdmin = "ONLY-SUPER-ADMIN-CAN-ACCESS";
        public const string CompanyAdmin = "ONLY-COMPANY-ADMIN-CAN-ACCESS";
        public const string CompanyAdminOrEmployee = "COMPANY-ADMIN-AND-EMPLOYEE-CAN-ACCESS";
        public const string Employee = "ONLY-EMPLOYEE-CAN-ACCESS";
        public const string AllowAnonymous = "ALLOW-ANONYMOUS";

        internal static IServiceCollection AddAuthPolicy(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(AllRoles, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.SuperAdmin.ToString()) ||
                        context.User.IsInRole(RoleName.CompanyAdmin.ToString()) ||
                        context.User.IsInRole(RoleName.Employee.ToString());
                }))
                .AddPolicy(SuperAdmin, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.SuperAdmin.ToString());
                }))
                .AddPolicy(CompanyAdmin, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.CompanyAdmin.ToString());
                }))
                .AddPolicy(CompanyAdminOrEmployee, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.CompanyAdmin.ToString()) ||
                        context.User.IsInRole(RoleName.Employee.ToString());
                }))
                .AddPolicy(Employee, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.Employee.ToString());
                }));

            return services;
        }
    }
}

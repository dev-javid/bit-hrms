using Microsoft.AspNetCore.Mvc.Authorization;
using static Domain.Common.Enums;

namespace Presentation.DependencyRegistration
{
    internal static class Mvc
    {
        internal static void AddMvcWithAuthPolicy(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(AuthPolicy.AllRoles, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.SuperAdmin.ToString()) ||
                        context.User.IsInRole(RoleName.CompanyAdmin.ToString()) ||
                        context.User.IsInRole(RoleName.Employee.ToString());
                }))
                .AddPolicy(AuthPolicy.SuperAdmin, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.SuperAdmin.ToString());
                }))
                .AddPolicy(AuthPolicy.CompanyAdmin, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.CompanyAdmin.ToString());
                }))
                .AddPolicy(AuthPolicy.CompanyAdminOrEmployee, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.CompanyAdmin.ToString()) ||
                        context.User.IsInRole(RoleName.Employee.ToString());
                }))
                .AddPolicy(AuthPolicy.Employee, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleName.Employee.ToString());
                }));

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
        }
    }
}

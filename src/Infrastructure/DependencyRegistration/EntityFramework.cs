using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Encryption;
using Infrastructure.EntityFramework.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyRegistration
{
    internal static class EntityFramework
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditInterceptor>();
            services.AddScoped<DomainEventsInterceptor>();
            services.AddDbContext<DatabaseContext>((sp, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
                var auditInterceptor = sp.GetService<AuditInterceptor>();
                var domainEventsInterceptor = sp.GetService<DomainEventsInterceptor>();
                options.AddInterceptors(auditInterceptor!);
                options.AddInterceptors(domainEventsInterceptor!);
            });

            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<DatabaseContext>());

            var key = configuration.GetValue<string>("Encryption:Key")!;
            var iv = configuration.GetValue<string>("Encryption:IV")!;
            services.AddSingleton<IEncryptionProvider>(x => new AesEncryptionProvider(Convert.FromBase64String(key), Convert.FromBase64String(iv)));
            return services;
        }
    }
}

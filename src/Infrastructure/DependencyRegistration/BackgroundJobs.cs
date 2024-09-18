using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyRegistration
{
    internal static class BackgroundJobs
    {
        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                    config.UsePostgreSqlStorage(c =>
                        c.UseNpgsqlConnection(configuration.GetConnectionString("Jobs")));
            });

            services.AddHangfireServer(option =>
            {
                option.ShutdownTimeout = TimeSpan.FromSeconds(20);
                option.StopTimeout = TimeSpan.FromSeconds(30);
            });

            return services;
        }
    }
}

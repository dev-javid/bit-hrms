using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyRegistration;

public static class Registeration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEntityFramework(configuration)
            .AddIdentity(configuration)
            .AddBackgroundJobs(configuration)
            .AddOtherServices(configuration);

        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}

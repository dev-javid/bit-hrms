using System.Reflection;
using Application.Common.MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR((options) =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TrimmingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);
        return services;
    }
}

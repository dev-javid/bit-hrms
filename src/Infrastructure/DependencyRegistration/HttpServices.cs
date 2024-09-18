using Application.Common.Configuration;
using Infrastructure.HttpHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyRegistration;

public static class HttpServices
{
    public static void AddHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        WhatsAppConfiguration whatsAppConfig = new();
        configuration.GetSection("WhatsApp").Bind(whatsAppConfig);
        services.AddSingleton(whatsAppConfig);

        services.AddHttpClient("WhatsApp", client =>
        {
            client.BaseAddress = new Uri(whatsAppConfig.BaseUrl);
        })
        .AddHttpMessageHandler(() => new LoggingDelegatingHandler())
        .AddHttpMessageHandler(() => new AuthenticationDelegatingHandler(whatsAppConfig.AccessToken));
    }
}

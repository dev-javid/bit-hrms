using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Infrastructure.BackgroundJobs.DomainEventProcessorJob;

namespace Infrastructure.DependencyRegistration
{
    internal static class OtherServices
    {
        internal static IServiceCollection AddOtherServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IStaticContentReader, StaticContentReader>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();

            var emailConfig = new EmailService.EmailConfiguration();
            configuration.GetSection("Email").Bind(emailConfig);
            services.AddSingleton(emailConfig);

            var domainEventConfig = new DomainEventConfiguration();
            configuration.GetSection("DomainEvent").Bind(domainEventConfig);
            services.AddSingleton(domainEventConfig);

            services.AddSingleton<IFileService>((_) =>
            {
                var apiUrl = configuration.GetValue<string?>("ApplicationUrls:ApiUrl");
                var request = _.GetService<IHttpContextAccessor>()!.HttpContext!.Request;
                var url = $"{request.Scheme}://{request.Host}{request.PathBase}";
                return new FileService(configuration.GetValue<string>("FileStorage:RootDirectory")!, string.IsNullOrWhiteSpace(apiUrl) ? url : apiUrl);
            });

            services.AddHttpServices(configuration);

            return services;
        }
    }
}

using Application;
using Infrastructure.BackgroundJobs;
using Infrastructure.DependencyRegistration;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Presentation.DependencyRegistration;
using Presentation.Middleware;
using Serilog.Debugging;
using Winton.Extensions.Configuration.Consul;

namespace Presentation
{
    public class Program
    {
        protected Program()
        {
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SetupConsul(builder);
            SetupLogging(builder);
            SetupDatabase(builder);

            builder.Services
                .AddPresentationServices(builder)
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();
            JobScheduler.SetSchedule(app);
            app.ConfigureMiddleware();

            app.Run();
        }

        private static void SetupDatabase(WebApplicationBuilder builder)
        {
            Database.DatabaseManager.Setup(builder.Configuration);
            Database.LogDbManager.Setup(builder.Configuration);
        }

        private static void SetupLogging(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Configuration)
                            .CreateLogger();
            SelfLog.Enable(Console.Error);
        }

        private static void SetupConsul(WebApplicationBuilder builder)
        {
            var consulAddress = Environment.GetEnvironmentVariable("CONSUL_URL")!;

            if (!string.IsNullOrWhiteSpace(consulAddress))
            {
                var env = builder.Environment;

                builder.Configuration.AddConsul($"{env.EnvironmentName}/Common", options =>
                {
                    options.ReloadOnChange = true;
                    options.Optional = true;
                    options.ConsulConfigurationOptions = (config) =>
                    {
                        config.Address = new Uri(consulAddress);
                    };
                });

                builder.Configuration.AddConsul($"{env.EnvironmentName}/BIT-HRMS", options =>
                {
                    options.ReloadOnChange = true;
                    options.Optional = true;
                    options.ConsulConfigurationOptions = (config) =>
                    {
                        config.Address = new Uri(consulAddress);
                    };
                });
            }
        }
    }
}

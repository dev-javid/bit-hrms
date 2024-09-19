using Application.Common.Abstract;
using Database;
using Hangfire;
using Infrastructure.EntityFramework.Encryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tests.Integration.MockServices;

namespace Tests.Integration.Fixture
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public TestWebApplicationFactory()
        {
            Database = new TestDatabase();
        }

        public TestDatabase Database { get; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Tests.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            IConfiguration configuration = configBuilder.Build();

            builder.UseConfiguration(configuration);

            var dupSuccess = DatabaseManager.Setup(configuration);
            if (!dupSuccess)
            {
                throw new Exception("Couldn't execute DbUp successfully!");
            }

            TestWebApplicationFactory<TStartup>.ReplaceServices(builder);
            var connectionStringSetting = configuration.GetConnectionString("Default")!;
            Database.Setup(connectionStringSetting).Wait();
        }

        private static void ReplaceServices(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(services);
                services.AddScoped<IBackgroundJobClient, MockHangfireClient>();
                services.AddScoped<IEmailService, MockEmailService>();
                services.AddSingleton<IEncryptionProvider>(x => new AesEncryptionProvider(Convert.FromBase64String("QQsOzPwQgHNqPMhP/QlfDhApMT6PPep4HSqYBBAhUF0="), Convert.FromBase64String("ilfOg5HA9VMnoEOnvhphqQ==")));
            });
        }
    }
}

using System.Net.Http.Headers;
using Application.Auth.Commands;
using Application.Common.Abstract;
using Bogus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tests.Integration.MockServices;

namespace Tests.Integration.Fixture
{
    [Collection("TestCollection")]
    public class IntegrationTest
    {
        public IntegrationTest(TestWebApplicationFactory<Program> factory)
        {
            Factory = factory;
            Client = factory.CreateClient();
            Database = factory.Database;
            Client.DefaultRequestHeaders.Add("X-DeviceId", "TEST-DEVICE-ID");
        }

        public TestWebApplicationFactory<Program> Factory { get; }

        protected static string JsoneMediaType => "application/json";

        protected HttpClient Client { get; }

        protected TestDatabase Database { get; }

        protected Faker Faker { get; set; } = new Faker();

        protected async Task LoginAsync(RoleName roleName)
        {
            if (roleName == RoleName.SuperAdmin)
            {
                await LoginAsSuperAdminAsync();
            }
            else if (roleName == RoleName.CompanyAdmin)
            {
                await LoginAsCompanyAdminAsync();
            }
            else if (roleName == RoleName.Employee)
            {
                await LoginAsEmployeeAsync();
            }
        }

        protected async Task LoginAsSuperAdminAsync()
        {
            var command = new SigninCommand
            {
                Email = "super-admin@example.com",
                Password = "Password@123"
            };
            await LoginAsync(command);
        }

        protected async Task LoginAsCompanyAdminAsync()
        {
            var command = new SigninCommand
            {
                Email = "company-admin@example.com",
                Password = "Password@123"
            };
            await LoginAsync(command);
        }

        protected async Task LoginAsEmployeeAsync()
        {
            var command = new SigninCommand
            {
                Email = "employee@example.com",
                Password = "Password@123"
            };
            await LoginAsync(command);
        }

        protected async Task FeedDataAsync(string scriptPath)
        {
            var scope = Factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
            await Database.FeedDataAsync(context, scriptPath);
        }

        protected async Task ResetDatabaseAsync()
        {
            await Database.ResetDatabaseAsync();
        }

        protected async Task Authorization_Test_Base(RoleName role, bool authorized, string url, HttpMethod httpMethod)
        {
            await LoginAsync(role);

            var scope = Factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var services = scope.ServiceProvider.GetService<IServiceCollection>()!;
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehaviour<,>));

            if (httpMethod == HttpMethod.Get)
            {
                using HttpResponseMessage response = await Client.GetAsync(url);
                if (authorized)
                {
                    response.StatusCode.Should().NotBe(HttpStatusCode.Forbidden);
                }
                else
                {
                    response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
                }
            }
            else if (httpMethod == HttpMethod.Delete)
            {
                using HttpResponseMessage response = await Client.DeleteAsync(url);
                if (authorized)
                {
                    response.StatusCode.Should().NotBe(HttpStatusCode.Forbidden);
                }
                else
                {
                    response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
                }
            }
            else if (httpMethod == HttpMethod.Post)
            {
                using StringContent content = new StringContent(JsonSerializer.Serialize(new { }), Encoding.UTF8, JsoneMediaType);
                using var response = await Client.PostAsync(url, content);
                if (authorized)
                {
                    response.StatusCode.Should().NotBe(HttpStatusCode.Forbidden);
                }
                else
                {
                    response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
                }
            }
            else if (httpMethod == HttpMethod.Put)
            {
                using StringContent content = new StringContent(JsonSerializer.Serialize(new { }), Encoding.UTF8, JsoneMediaType);
                using var response = await Client.PutAsync(url, content);
                if (authorized)
                {
                    response.StatusCode.Should().NotBe(HttpStatusCode.Forbidden);
                }
                else
                {
                    response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
                }
            }
        }

        private async Task LoginAsync(SigninCommand command)
        {
            var scope = Factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
            await Database.ResetDatabaseAsync();
            await Database.FeedDataAsync(context, "./Fixture/Setup.sql");

            HttpResponseMessage response;
            using (StringContent content = new(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PostAsync("api/auth/sign-in", content);
            }

            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Signin failed");
            }

            var signInResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SigninCommand.JwtTokens>(result);

            if (signInResponse == null)
            {
                throw new Exception("Signin failed");
            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", signInResponse.AccessToken);
        }
    }
}

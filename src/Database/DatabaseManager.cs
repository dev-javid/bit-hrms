using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Database
{
    public static class DatabaseManager
    {
        public static bool Setup(IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("Default")!;

            Console.WriteLine("**********************");
            Console.WriteLine(defaultConnectionString);

            EnsureDatabase.For.PostgresqlDatabase(defaultConnectionString);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(defaultConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Log.Fatal(result.Error, "Dbup Error");
                return false;
            }

            Log.Information("Dbup Success");
            return true;
        }
    }
}

using DbUp;
using Microsoft.Extensions.Configuration;

namespace Database
{
    public static class LogDbManager
    {
        public static void Setup(IConfiguration configuration)
        {
            var logTable = configuration.GetValue<string>("Serilog:WriteTo:0:Args:tableName")!;
            var logConnectionString = configuration.GetValue<string>("Serilog:WriteTo:0:Args:connectionString");

            EnsureDatabase.For.PostgresqlDatabase(logConnectionString);

            using var connection = new Npgsql.NpgsqlConnection(logConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = GetTableScript(logTable);
            command.ExecuteNonQuery();
        }

        private static string GetTableScript(string logTable)
        {
            return $@"CREATE TABLE IF NOT EXISTS public.{logTable} (
	                ""Message"" text NULL,
	                ""MessageTemplate"" text NULL,
	                ""Level"" int4 NULL,
	                ""Timestamp"" timestamptz NULL,
	                ""Exception"" text NULL,
	                ""LogEvent"" jsonb NULL
                );";
        }
    }
}

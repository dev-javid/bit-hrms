using System.Configuration;
using Application.Common.Abstract;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;

namespace Tests.Integration.Fixture
{
    public class TestDatabase
    {
        private Respawner _respawner = null!;
        private string _connectionStringSetting = null!;

        internal async Task Setup(string connectionStringSetting)
        {
            using var connection = new NpgsqlConnection(connectionStringSetting);
            await connection.OpenAsync();

            _connectionStringSetting = connectionStringSetting;
            _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                SchemasToInclude =
                [
                    "public"
                ],
                TablesToIgnore = ["schemaversions"],
                DbAdapter = DbAdapter.Postgres
            });
        }

        internal async Task ResetDatabaseAsync()
        {
            using var connection = new NpgsqlConnection(_connectionStringSetting);
            await connection.OpenAsync();
            await _respawner.ResetAsync(connection);
        }

        internal async Task FeedDataAsync(IDbContext dbContext, string relativeScriptPath)
        {
            var dataLoadScript = await File.ReadAllTextAsync(relativeScriptPath);
            await dbContext.Database.ExecuteSqlRawAsync(dataLoadScript);
        }
    }
}

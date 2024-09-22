using Serilog.Ui.Core.Extensions;
using Serilog.Ui.PostgreSqlProvider.Extensions;
using Serilog.Ui.Web.Extensions;

namespace Presentation.DependencyRegistration
{
    internal static class SerilogUI
    {
        internal static void AddSerilogUI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSerilog();

            services.AddSerilogUi(options =>
            {
                var logTable = configuration.GetValue<string>("Serilog:WriteTo:0:Args:tableName")!;
                var connectionString = configuration.GetValue<string>("Serilog:WriteTo:0:Args:connectionString")!;

                options.UseNpgSql(options => options.WithConnectionString(connectionString)
                    .WithTable(logTable))
                    .AddScopedBasicAuthFilter();
            })
            .AddControllersWithViews();
        }
    }
}

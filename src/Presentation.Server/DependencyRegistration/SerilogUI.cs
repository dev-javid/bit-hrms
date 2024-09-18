using Serilog;
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

            var logTable = configuration.GetValue<string>("Serilog:WriteTo:0:Args:tableName")!;
            var connectionString = configuration.GetValue<string>("Serilog:WriteTo:0:Args:connectionString")!;

            services.AddSerilogUi(options =>
                options.UseNpgSql(options => options.WithConnectionString(connectionString).WithTable(logTable)));
        }
    }
}

using Hangfire;
using Microsoft.Extensions.FileProviders;
using Serilog.Ui.Web.Extensions;

namespace Presentation.Middleware
{
    public static class Middleware
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseCors("CORS");
            app.UseCustomStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseHangfireDashboard();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilogUi(options =>
            {
                options.WithAuthenticationType(Serilog.Ui.Web.Models.AuthenticationType.Basic)
                    .WithExpandedDropdownsByDefault();
            });
            app.MapControllers();
            app.MapFallbackToFile("/index.html");
            app.UseHealthChecks("/health");
        }

        private static void UseCustomStaticFiles(this WebApplication app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            using var scope = app.Services.CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var roorDirectory = configuration.GetValue<string>("FileStorage:RootDirectory")!;

            if (!Directory.Exists(roorDirectory))
            {
               Directory.CreateDirectory(roorDirectory);
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(new DirectoryInfo(roorDirectory).FullName),
                RequestPath = new PathString("/media")
            });
        }
    }
}

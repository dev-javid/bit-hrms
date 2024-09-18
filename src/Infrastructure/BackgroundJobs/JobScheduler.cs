using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs
{
    public static class JobScheduler
    {
        public static void SetSchedule(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            scope.ServiceProvider.GetService<IRecurringJobManager>()
                    .AddOrUpdate<DomainEventProcessorJob>(
                        "DomainEventProcessorJob",
                        (x) => x.Run(),
                        "*/30 * * * * *",
                        new RecurringJobOptions());

            scope.ServiceProvider.GetService<IBackgroundJobClient>().
                Enqueue<SeedDataJob>(x => x.Run());
        }
    }
}

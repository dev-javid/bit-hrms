using FluentValidation;
using Infrastructure.EntityFramework;
using MediatR;
using static Infrastructure.BackgroundJobs.DomainEventProcessorJob;

namespace Infrastructure.BackgroundJobs
{
    internal class DomainEventProcessorJob(IDbContext dbContext, IPublisher publisher, DomainEventConfiguration configuration)
    {
        public async Task Run()
        {
            var events = await dbContext
                .Set<DomainEvent>()
                .Where(a => a.ProcessCompletedOnUtc == null &&
                            (a.ProcessStartedOnUtc == null || (a.Error != null && a.RetryOnError && a.RetryCount < configuration.RetryCount)))
                .OrderBy(x => x.OccurredOn)
                .Take(5)
                .ToListAsync();

            foreach (var @event in events)
            {
                var notification = JsonSerializer.Deserialize(@event.JSON, Type.GetType(@event.Type)!);

                if (notification is not null)
                {
                    @event.SetProcessStarted();
                    await dbContext.SaveChangesAsync(default);
                    try
                    {
                        await publisher.Publish(notification, default);
                        @event.SetProcessCompleted();
                    }
                    catch (CustomException exception)
                    {
                        @event.SetError(exception.Message);
                    }
                    catch (ValidationException exception)
                    {
                        string data = string.Join("\n", exception.Errors);
                        @event.SetError(data);
                    }
                    catch (Exception exception)
                    {
                        exception.LogException();
                        @event.SetError(exception.UnwrapExceptionMessage());
                    }

                    await dbContext.SaveChangesAsync(default);
                }
            }

            if (events.Count == 0)
            {
                await Task.Delay(5000);
            }
        }

        internal class DomainEventConfiguration
        {
            public int RetryCount { get; set; }
        }
    }
}

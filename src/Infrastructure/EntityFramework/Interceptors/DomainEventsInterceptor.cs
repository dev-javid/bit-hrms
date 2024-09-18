using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.EntityFramework.Interceptors
{
    public sealed class DomainEventsInterceptor : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var dbContext = eventData.Context!;

            AddDomainEvents<int>(dbContext);
            AddDomainEvents<Guid>(dbContext);

            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context!;

            AddDomainEvents<int>(dbContext);
            AddDomainEvents<Guid>(dbContext);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void AddDomainEvents<T>(DbContext dbContext) where T : struct
        {
            var events = dbContext
                            .ChangeTracker
                            .Entries<Entity<T>>()
                            .Select(x => x.Entity)
                            .SelectMany(x =>
                            {
                                var domainEvents = x.GetDomainEvents();
                                x.ClearDomainEvents();
                                return domainEvents;
                            })
                            .Select(x => DomainEvent.Create(x.GetType(), x, x.RetryOnError))
                            .ToList();

            if (events.Any())
            {
                dbContext.Set<DomainEvent>().AddRange(events);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.EntityFramework.Interceptors;

public class AuditInterceptor(ICurrentUser currentUser) : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        UpdateEntities(eventData.Context);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext == null)
        {
            return;
        }

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            var entityType = entry.Metadata;

            var isValueObject = entityType.ClrType.IsSubclassOf(typeof(ValueObject)) || entityType.ClrType == typeof(DomainEvent);
            if (isValueObject)
            {
                continue;
            }

            if (entityType.ClrType.IsAssignableTo(typeof(CompanyEntity<int>)) || entityType.ClrType.IsAssignableTo(typeof(CompanyEntity<Guid>)))
            {
                entry.Property("CompanyId").CurrentValue = currentUser.CompanyId;
            }

            if (entityType.ClrType.IsAssignableTo(typeof(Entity<int>)) || entityType.ClrType.IsAssignableTo(typeof(Entity<Guid>)))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedBy").CurrentValue = currentUser.Id;
                    entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                    entry.Property("ModifiedBy").CurrentValue = null;
                    entry.Property("ModifiedOn").CurrentValue = null;
                }
                else if (entry.State != EntityState.Deleted)
                {
                    entry.Property("ModifiedBy").CurrentValue = currentUser.Id;
                    entry.Property("ModifiedOn").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}

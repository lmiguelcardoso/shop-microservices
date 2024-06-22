using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                var isNewEntry = entry.Entity.CreatedBy == null && entry.Entity.CreatedAt == null;
                if (entry.State == EntityState.Added || isNewEntry)
                {
                    entry.Entity.CreatedBy = "lmiguelcardoso";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || isNewEntry || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = "lmiguelcardoso";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }

    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(e =>
        e.TargetEntry != null &&
        e.TargetEntry.Metadata.IsOwned() &&
        (e.TargetEntry.State == EntityState.Added || e.TargetEntry.State == EntityState.Modified));
    }
}
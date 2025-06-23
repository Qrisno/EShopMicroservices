using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor: SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if(context==null) return;
        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            var entityAdded = entry.State == EntityState.Added;
            var entityModified = entry.State == EntityState.Modified;
            if (entityAdded)
            {
                entry.Entity.CreatedAt =DateTime.Now;
                entry.Entity.CreatedBy = "Sheep";
            }

            if (entityAdded || entityModified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "Sheep";
                entry.Entity.LastModified = DateTime.Now;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() &&
             (r.TargetEntry.State == EntityState.Modified || r.TargetEntry.State == EntityState.Added));
}
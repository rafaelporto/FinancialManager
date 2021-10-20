using FinancialManager.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Core.Data
{
    public abstract class ManagerContext<T> : DbContext, IUnitOfWork where T : DbContext
    {
        protected IScopeControl ScopeControl { get; }

        public ManagerContext(DbContextOptions<T> options, IScopeControl scopeControl)
            : base(options) =>
            ScopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl));

        public async Task<bool> Commit(CancellationToken token = default)
        {
            foreach (var entry in ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Property(p => p.TenantId).CurrentValue = ScopeControl.GetUserId();

                if (entry.State == EntityState.Modified)
                    entry.Property(p => p.LastUpdated).CurrentValue = DateTimeOffset.Now;

                if (entry.Property(p => p.TenantId).CurrentValue != ScopeControl.GetUserId())
                    throw new InvalidOperationException("Unauthorized operation.");
            }

            return await base.SaveChangesAsync(token) > 0;
        }
    }
}
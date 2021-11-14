using FinancialManager.Core;
using FinancialManager.Core.Data;
using FinancialManager.Core.DomainObjects;
using FinancialManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Data
{
    public class FinancialManagerContext : DbContext, IUnitOfWork
    {
        public FinancialManagerContext(DbContextOptions<FinancialManagerContext> options,
            IScopeControl scopeControl)
            : base(options) =>
            ScopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl));

        private IScopeControl ScopeControl { get; }
        public DbSet<FinancialAccount> Accounts { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration(ScopeControl.GetUserId()));
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration(ScopeControl.GetUserId()));
            modelBuilder.ApplyConfiguration(new TagConfiguration(ScopeControl.GetUserId()));

            base.OnModelCreating(modelBuilder);
        }

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

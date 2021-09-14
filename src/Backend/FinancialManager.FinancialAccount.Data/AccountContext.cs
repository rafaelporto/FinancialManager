using FinancialManager.Core;
using FinancialManager.Core.Data;
using FinancialManager.FinancialAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.FinancialAccounts.Data
{
    public class AccountContext : DbContext, IUnitOfWork
    {
        private readonly IScopeControl _scopeControl;

        public AccountContext(DbContextOptions<AccountContext> options, IScopeControl scopeControl)
            : base(options) =>
            _scopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl));

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration(_scopeControl.GetUserId()));

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit(CancellationToken token = default)
        {
            SetLastUpdated();
            return await base.SaveChangesAsync(token) > 0;
        }

        protected void SetLastUpdated()
        {
            foreach (var entry in ChangeTracker.Entries<Account>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Property(p => p.LastUpdated).CurrentValue = DateTimeOffset.Now;
            }
        }
    }
}

using FinancialManager.Core;
using FinancialManager.Core.Data;
using FinancialManager.FinancialAccounts.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.FinancialAccounts.Data
{
    public class AccountContext : ManagerContext<AccountContext>
    {
        public AccountContext(DbContextOptions<AccountContext> options, IScopeControl scopeControl)
            : base(options, scopeControl) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration(ScopeControl.GetUserId()));
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration(ScopeControl.GetUserId()));
            modelBuilder.ApplyConfiguration(new CategoryConfiguration(ScopeControl.GetUserId()));

            base.OnModelCreating(modelBuilder);
        }
    }
}

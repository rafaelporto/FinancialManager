using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Infrastructure.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            SetLastUpdated();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetLastUpdated();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected void SetLastUpdated()
        {
            foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Property(p => p.LastUpdated).CurrentValue = DateTimeOffset.Now;
            }
        }
    }
}

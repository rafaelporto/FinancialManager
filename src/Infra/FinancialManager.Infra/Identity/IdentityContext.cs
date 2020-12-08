using System;
using System.Linq;
using System.Threading.Tasks;
using FinancialManager.Infra.CrossCutting.Core;
using FinancialManager.Infra.CrossCutting.Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinancialManager.Infra.CrossCutting.Identity
{
	internal class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IUnitOfWork
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(p =>
            {
                p.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
                p.Property(user => user.CreatedDate).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            builder.Entity<IdentityRole<Guid>>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Identity");
            base.OnConfiguring(optionsBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach(var entry in ChangeTracker.Entries().Where(p => p.Entity is IEntity))
            {
                if (entry.State == EntityState.Added)
                    ((IEntity)entry.Entity).CreatedDate = DateTimeOffset.UtcNow;

                if (entry.State == EntityState.Modified)
                    ((IEntity)entry.Entity).UpdatedDate = DateTimeOffset.UtcNow;
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}

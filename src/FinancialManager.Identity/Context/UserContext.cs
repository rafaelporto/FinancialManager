using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Identity.Context
{
	public class UserContext : IdentityDbContext<ApplicationUser>
	{
		public UserContext(DbContextOptions<UserContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}

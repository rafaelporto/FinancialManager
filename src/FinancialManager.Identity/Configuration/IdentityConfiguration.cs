using FinancialManager.Identity.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Identity.Configuration
{
	public static class IdentityConfiguration
	{
		public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
		{
			services.AddIdentity<ApplicationUser, RoleUser>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredLength = 8;

				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedEmail = true;
			});

			services.AddDbContext<UserContext>(options =>
				options.UseInMemoryDatabase("Identity"));

			return services;
		}

		public static IApplicationBuilder UseApplicationIdentity(this IApplicationBuilder app) =>
			app.UseAuthentication();
	}
}

using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Identity.Configuration
{
	public static class IdentityConfiguration
	{
		public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
		{
			services.AddIdentity(options =>
			{
				
			});

			return services;
		}
	}
}

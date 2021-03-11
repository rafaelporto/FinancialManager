using FinancialManager.Identity;
using FinancialManager.Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Infra.CrossCutting.IoC
{
    public static class DependencyBootstrapper
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IAuthService, AuthService>();
			services.AddSingleton(provider => DocumentStoreHolder.Store);
			services.AddIdentityConfiguration(configuration);

			return services;
		}

		public static IApplicationBuilder UseServicesMiddleware(this IApplicationBuilder app)
		{
			return app.UseIdentityConfiguration();
		}
	}
}

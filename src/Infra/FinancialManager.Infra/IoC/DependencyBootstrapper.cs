using FinancialManager.Identity;
using FinancialManager.Infra.Data;
using FinancialManager.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;

namespace FinancialManager.Infra.CrossCutting.IoC
{
	public static class DependencyBootstrapper
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<INotificationContext, NotificationContext>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddSingleton(provider => DocumentStoreHolder.Store);
			services.AddIdentityConfiguration(configuration);

			return services;
		}
	}
}

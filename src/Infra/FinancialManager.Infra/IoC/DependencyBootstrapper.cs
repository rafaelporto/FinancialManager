using FinancialManager.Identity;
using FinancialManager.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Infra.CrossCutting.IoC
{
	public static class DependencyBootstrapper
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<INotificationContext, NotificationContext>();
			services.AddIdentityConfiguration(configuration);

			return services;
		}
	}
}

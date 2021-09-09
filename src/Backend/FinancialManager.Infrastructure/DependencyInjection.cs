using FinancialManager.Core;
using FinancialManager.Core.Communication;
using FinancialManager.Infrastructure.Identity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddIdentityLayer(configuration);
            services.AddMediatR(typeof(NotificationHandler));

            return services;
        }
    }
}

using FinancialManager.Core;
using FinancialManager.FinancialAccounts.Data;
using FinancialManager.FinancialAccounts.Application;
using FinancialManager.FinancialAccounts.Domain;
using FinancialManager.Infrastructure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IScopeControl, ScopeControl>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.RegisterFinancialAccountServices(configuration);

            services.AddIdentityLayer(configuration);

            return services;
        }

        internal static IServiceCollection RegisterFinancialAccountServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblyDbContext = typeof(AccountContext).Assembly.GetName().Name;

            services.AddDbContext<AccountContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("Default"), b =>
                            {
                                b.MigrationsAssembly(assemblyDbContext);
                                b.MigrationsHistoryTable("MigrationAccountHistory");
                            }));

            services.AddScoped<IAccountAppService, AccoutAppService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}

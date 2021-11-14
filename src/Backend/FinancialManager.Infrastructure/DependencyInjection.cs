using FinancialManager.Core;
using FinancialManager.Data;
using FinancialManager.Application;
using FinancialManager.Domain;
using FinancialManager.Infrastructure.Identity;
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

            var assemblyDbContext = typeof(FinancialManagerContext).Assembly.GetName().Name;

            services.AddDbContext<FinancialManagerContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("Default"), b =>
                            {
                                b.MigrationsAssembly(assemblyDbContext);
                                b.MigrationsHistoryTable("MigrationHistory");
                            }));

            services.AddScoped<IAccountAppService, AccoutAppService>();
            services.AddScoped<IAccountRepository, FinancialAccountRepository>();

            services.AddScoped<ITagAppService, TagAppService>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddIdentityLayer(configuration);

            return services;
        }
    }
}

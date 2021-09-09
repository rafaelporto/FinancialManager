using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static FinancialManager.Infrastructure.Identity.InitializerOptionsFactory;

namespace FinancialManager.Infrastructure.Identity
{
    public static class InitializerIdentity
    {
        public static IServiceCollection AddIdentityLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services), "The service collection is required");

            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration), "The configuration is required");

            var assemblyDbContext = typeof(IdentityContext).Assembly.GetName().Name;

            services.ConfigureIdentityOptions(configuration, out var jwtSettings);

            services.AddDbContext<IdentityContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("Default"), b =>
                            b.MigrationsAssembly(assemblyDbContext)));

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => SetIdentityOptions(ref options))
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(authOptions => SetAuthenticationOptions(ref authOptions))
                    .AddJwtBearer(bearerOptions => SetJwtOptions(ref bearerOptions, jwtSettings));

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = BuildRequiredAuthenticatePolicy();
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityLayer(this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app),
                    "Application Builder is required for configure identity middleware.");

            return app.UseAuthentication()
                      .UseAuthorization();
        }

        internal static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services, 
            IConfiguration configuration, out JwtSettings jwtSettings)
        {
            if (!configuration.GetSection(JwtSettings.CONFIG_NAME).Exists())
                throw new ConfigurationNotFoundException("Jwt settings not found.");

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.CONFIG_NAME));

            jwtSettings = configuration.GetSection(JwtSettings.CONFIG_NAME).Get<JwtSettings>();

            return services;
        }
    }
}

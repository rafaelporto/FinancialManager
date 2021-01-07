using System;
using FinancialManager.Identity.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;

namespace FinancialManager.Identity
{
	internal static class Abstractions
	{
		internal static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (services is null)
				throw new ArgumentException($"{nameof(services)} is required for configure identity.");

			services.Configure<AppJwtSettings>(configuration.GetSection(AppJwtSettings.CONFIG_NAME));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
				options.User.RequireUniqueEmail = false;

			}).UseRavenDBDataStoreAdaptor<IDocumentStore>();

			services.AddScoped<IUserService, UserService>();

			return services;
		}

		internal static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
		{
			if (app is null)
				throw new ArgumentException($"{nameof(app)} is required for configure identity middleware.");

			return app.UseAuthentication()
					  .UseAuthorization();
		}

		private static IdentityBuilder UseRavenDBDataStoreAdaptor<TDocumentStore>(this IdentityBuilder builder)
			where TDocumentStore : class, IDocumentStore => 
			builder.AddRavenDBUserStore<TDocumentStore>()
				   .AddRavenDBRoleStore<TDocumentStore>();

		private static IdentityBuilder AddRavenDBUserStore<TDocumentStore>(this IdentityBuilder builder)
		{
			var userStoreType = typeof(UserStore<,,>).MakeGenericType(builder.UserType, builder.RoleType, typeof(TDocumentStore));

			builder.Services.AddScoped(typeof(IUserStore<>).MakeGenericType(builder.UserType),
				userStoreType);

			return builder;
		}

		private static IdentityBuilder AddRavenDBRoleStore<TDocumentStore>(this IdentityBuilder builder)
		{
			var roleStoreType = typeof(RoleStore<,>).MakeGenericType(builder.RoleType, typeof(TDocumentStore));

			builder.Services.AddScoped(typeof(IRoleStore<>).MakeGenericType(builder.RoleType),
				roleStoreType);

			return builder;
		}
	}
}

using Blazored.LocalStorage;
using BlazorState;
using FinancialManager.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;

namespace FinancialManager.Web.Client.Extensions
{
    public static class ServicesExtensions
    {
		private const string API_URL = "https://localhost:5025";


		public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(sp => new HttpClient { BaseAddress = new Uri(API_URL) });

			serviceCollection.ConfigureRefit();
			serviceCollection.ConfigureBlazorise();

			serviceCollection.AddBlazoredLocalStorage(config =>
			{
				config.JsonSerializerOptions.WriteIndented = true;
			});

			serviceCollection.AddOptions();
			serviceCollection.AddAuthorizationCore();

			serviceCollection.AddScoped<AuthStateProvider>();
			serviceCollection.AddScoped<AuthenticationStateProvider>
				(provider => provider.GetRequiredService<AuthStateProvider>());

			serviceCollection.AddBlazorState
			  ((aOptions) =>
				{
					aOptions.UseReduxDevToolsBehavior = true;
					aOptions.Assemblies = new Assembly[]
					  {
						typeof(Program).GetTypeInfo().Assembly,
					  };
				}
			  );

            return serviceCollection;
        }
    }
}

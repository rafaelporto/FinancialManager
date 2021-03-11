using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorState;
using FinancialManager.Client.Handlers;
using FinancialManager.Client.Services;
using FinancialManager.Endpoints.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FinancialManager.Web.Client
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5025") });

			builder.Services.AddRefitClient<IAuth>()
							.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5025"))
							.AddHttpMessageHandler<AuthHeaderHandler>();

			builder.Services.AddBlazoredLocalStorage(config =>
			{
				config.JsonSerializerOptions.WriteIndented = true;
			});

			builder.Services.AddOptions();
			builder.Services.AddAuthorizationCore();

			builder.Services.AddScoped<AuthStateProvider>();
			builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());
			builder.Services.AddScoped<SignOutSessionStateManager>();
			builder.Services.AddTransient<AuthHeaderHandler>();

			builder.Services.AddBlazorState
			  (
				(aOptions) =>
				{
					aOptions.UseReduxDevToolsBehavior = true;
					aOptions.Assemblies =
					  new Assembly[]
					  {
					  typeof(Program).GetTypeInfo().Assembly,
					  };
				}
			  );

			await builder.Build().RunAsync();
		}
	}
}

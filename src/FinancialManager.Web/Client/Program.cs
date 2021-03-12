using System.Threading.Tasks;
using FinancialManager.Web.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FinancialManager.Web.Client
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.ConfigureServices();

			await builder.Build().RunAsync();
		}
	}
}

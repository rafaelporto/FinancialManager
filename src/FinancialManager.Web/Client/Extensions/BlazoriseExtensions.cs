using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Web.Client.Extensions
{
    public static class BlazoriseExtensions
    {
        public static IServiceCollection ConfigureBlazorise(this IServiceCollection serviceCollection) =>
             serviceCollection.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            }).AddBootstrapProviders()
              .AddFontAwesomeIcons();
    }
}

using FinancialManager.Client.Handlers;
using FinancialManager.Endpoints.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace FinancialManager.Web.Client.Extensions
{
    public static class RefitExtensions
    {
        public static IServiceCollection ConfigureRefit(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddRefitClient<IAuth>()
                            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5025"))
                            .AddHttpMessageHandler<AuthHeaderHandler>();

            return serviceCollection.AddTransient<AuthHeaderHandler>();
        }
    }
}

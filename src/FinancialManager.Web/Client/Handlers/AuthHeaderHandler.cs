using Blazored.LocalStorage;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Client.Handlers
{
    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _storageService;

        public AuthHeaderHandler(ILocalStorageService storageService) =>
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _storageService.GetItemAsStringAsync("access_token");

            if (token is not null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}

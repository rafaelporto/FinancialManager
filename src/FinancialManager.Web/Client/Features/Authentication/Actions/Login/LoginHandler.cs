using BlazorState;
using FinancialManager.Client.Services;
using FinancialManager.Endpoints.Authorization;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class LoginHandler : ActionHandler<LoginAction>
        {
            private readonly IAuth _authClient;
            private readonly AuthStateProvider _authStateProvider;

            private AuthState State => Store.GetState<AuthState>();

            public LoginHandler(IAuth httpClient, IStore store, AuthenticationStateProvider authProvider) : base(store)
            {
                _authClient = httpClient;
                _authStateProvider = authProvider as AuthStateProvider;
            }

            public override async Task<Unit> Handle(LoginAction loginAction, CancellationToken aCancellationToken)
            {
                var result = await _authClient.Login(loginAction.LoginRequest);

                if (result.IsSuccessed)
                {
                    await _authStateProvider.MarkUserAsAuthenticated(result.AccessToken);

                    var authState = await _authStateProvider.GetAuthenticationStateAsync();

                    State.Email = authState?.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                    State.Name = authState?.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value;
                    State.Roles = authState?.User?.Claims?.Where(c => c.Type == "role").Select(s => s.Value).ToList();
                }

                return await Unit.Task;
            }
        }
    }
}

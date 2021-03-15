using BlazorState;
using FinancialManager.Client.Services;
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
            private readonly AuthStateProvider _authStateProvider;

            private AuthState State => Store.GetState<AuthState>();

            public LoginHandler(IStore store, AuthenticationStateProvider authProvider) : base(store)
            {
                _authStateProvider = authProvider as AuthStateProvider;
            }

            public override async Task<Unit> Handle(LoginAction loginAction, CancellationToken aCancellationToken)
            {
                await _authStateProvider.MarkUserAsAuthenticated(loginAction.AccessToken);

                var authState = await _authStateProvider.GetAuthenticationStateAsync();

                State.Email = authState?.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                State.Name = authState?.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value;
                State.Roles = authState?.User?.Claims?.Where(c => c.Type == "role").Select(s => s.Value).ToList();

                return await Unit.Task;
            }
        }
    }
}

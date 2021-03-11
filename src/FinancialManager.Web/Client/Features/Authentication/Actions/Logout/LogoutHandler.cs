using BlazorState;
using FinancialManager.Client.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class LogoutHandler : ActionHandler<LogoutAction>
        {
            private readonly AuthStateProvider _authStateProvider;
            private readonly NavigationManager _navManager;

            private AuthState State => Store.GetState<AuthState>();

            public LogoutHandler(IStore store, AuthenticationStateProvider authProvider,
                                    NavigationManager navManager)
                : base(store)
            {
                _navManager = navManager;
                _authStateProvider = authProvider as AuthStateProvider;
            }

            public override async Task<Unit> Handle(LogoutAction logoutAction, CancellationToken aCancellationToken)
            {
                await _authStateProvider.MarkUserAsLoggedOut();

                State.Initialize();
                _navManager.NavigateTo("/");

                return await Unit.Task;
            }
        }
    }
}

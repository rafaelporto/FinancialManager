using BlazorState;
using FinancialManager.Endpoints.Authorization;

namespace FinancialManager.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class LoginAction : IAction
        {
            public LoginRequest LoginRequest { get; init; }

            public LoginAction(LoginRequest loginRequest) =>
                LoginRequest = loginRequest;
        }
    }
}

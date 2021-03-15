using BlazorState;
using FinancialManager.Endpoints.Authorization;

namespace FinancialManager.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class LoginAction : IAction
        {
            public string AccessToken { get; init; }

            public LoginAction(string accessToken) => AccessToken = accessToken;
        }
    }
}

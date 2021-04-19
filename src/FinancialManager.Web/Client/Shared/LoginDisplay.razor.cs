using FinancialManager.Client.Features.Authentication;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FinancialManager.Web.Client.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        AuthState AuthState => GetState<AuthState>();

        private async Task BeginSignOut()
        {
            await Mediator.Send(new AuthState.LogoutAction());
            Navigation.NavigateTo("/");
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinancialManager.Web.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthState { get; set; }
        private ClaimsPrincipal User { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            User = (await AuthState).User;

            await base.OnParametersSetAsync();
        }
    }
}

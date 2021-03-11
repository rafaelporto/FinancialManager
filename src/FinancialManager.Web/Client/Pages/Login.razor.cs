using FinancialManager.Endpoints.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using FinancialManager.Client.Features.Authentication;

namespace FinancialManager.Web.Client.Pages
{
    public partial class Login : ComponentBase
    {
        private readonly LoginRequest _loginRequest = new LoginRequest();

        private string ReturnUrl { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private IMediator Mediator { get; set; }

        protected override Task OnInitializedAsync()
        {
            ReturnUrl = HttpUtility.ParseQueryString(new Uri(Navigation.Uri).Query)["returnUrl"];
            return base.OnInitializedAsync();
        }

        async Task HandleValidSubmit(EditContext context)
        {
            await Mediator.Send(new AuthState.LoginAction(_loginRequest));

            Navigation.NavigateTo(ReturnUrl ?? Navigation.BaseUri);
        }
    }
}

using FinancialManager.Endpoints.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using FinancialManager.Client.Features.Authentication;
using Blazorise;
using System.Text.RegularExpressions;
using Refit;
using FinancialManager.Endpoints;

namespace FinancialManager.Web.Client.Pages
{
    public partial class Login : ComponentBase
    {
        private readonly LoginRequest _loginRequest = new LoginRequest();

        private string ReturnUrl { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private IAuth AuthClient { get; set; }

        [Inject]
        private IMediator Mediator { get; set; }

        private bool _showLoading = false;

        protected override Task OnInitializedAsync()
        {
            ReturnUrl = HttpUtility.ParseQueryString(new Uri(Navigation.Uri).Query)["returnUrl"];
            return base.OnInitializedAsync();
        }

        async Task SubmitLogin()
        {
            _showLoading = true;

            try
            {
                var result = await AuthClient.Login(_loginRequest);

                if (result.IsSuccessed)
                {
                    await Mediator.Send(new AuthState.LoginAction(result.Content.AccessToken));
                    _showLoading = false;
                    Navigation.NavigateTo(ReturnUrl ?? Navigation.BaseUri);
                }
            }
            catch (ApiException e)
            {
                var errorObj = await e.GetContentAsAsync<ApiResult<LoginResponse>>();
                Console.WriteLine($"Status code: {e.StatusCode}");
                Console.WriteLine($"Notifications: {errorObj.Notifications}");
                _showLoading = false;
            }
        }
        
        static void ValidateEmail(ValidatorEventArgs e)
        {
            var email = Convert.ToString(e.Value);

            var patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                + @"[a-zA-Z]{2,}))$";

            Regex regexStrict = new Regex(patternStrict);

            if (string.IsNullOrEmpty(email))
                e.Status = ValidationStatus.None;

            else if (!regexStrict.IsMatch(email))
                e.Status = ValidationStatus.Error;
        }
    }
}

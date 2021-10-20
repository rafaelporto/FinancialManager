using FinancialManager.Core;
using FinancialManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Api.Controllers
{
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IScopeControl scopeControl)
            : base(scopeControl)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<LoginResponseModel>), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken token = default)
        {
            LoginRequest request = new(model.Email, model.Password);
            var tokenResponse = await _authService.Login(request, token);
            return ApiResponse(tokenResponse);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model, CancellationToken token = default)
        {
            RegisterRequest request = new(model.Email, model.FirstName, model.LastName, model.Password, model.ConfirmPassword);
            await _authService.Register(request, token);

            return ApiResponse(HttpStatusCode.Created);
        }
    }
}

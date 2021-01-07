using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity;
using FinancialManager.Identity.Jwt;
using FinancialManager.Notifications;
using FinancialManager.Web.Shared.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinancialManager.Web.Server.Endpoints.Auth
{
	public class Login : BaseServerEndpoint
	{
		private readonly IAuthService _authService;

		public Login(IAuthService authService, INotificationContext notifications) : base(notifications) =>
			_authService = authService;

		[HttpPost("auth/login")]
		[SwaggerOperation(
			Summary = "Login",
			Description = "Login a user",
			OperationId = "auth.login",
			Tags = new[] { "AuthEndpoints" })
		]
		[ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> HandleAsync(LoginRequest request)
		{
			return await _authService.Login(request.Email, request.Password)
				.Finally<UserResponse, IActionResult>(result => result.IsSuccess ?
								Ok(LoginResponse.Success(result.Value.AccessToken,
												result.Value.ExpiresIn,
												result.Value.UserToken.Email,
												result.Value.UserToken.Roles)) :
								BadRequest(LoginResponse.Failure(_notifications.Messages)));
		}
	}
}

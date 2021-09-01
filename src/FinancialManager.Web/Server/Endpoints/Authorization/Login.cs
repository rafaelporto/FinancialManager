using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity;
using FinancialManager.Endpoints.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using FinancialManager.Infra.ValueObjects;
using FinancialManager.Endpoints;

namespace FinancialManager.Server.Endpoints.Authorization
{
    public class Login : BaseServerEndpoint<LoginRequest, LoginResponse>
	{
		private readonly IAuthService _authService;

		public Login(IAuthService authService) =>
			_authService = authService;

		[HttpPost("auth/login")]
		[SwaggerOperation(
			Summary = "Login",
			Description = "Login a user",
			OperationId = "auth.login",
			Tags = new[] { "AuthEndpoints" })
		]
		[ProducesResponseType(typeof(ApiResult<LoginResponse>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
		[AllowAnonymous]
		public async override Task<ApiResult<LoginResponse>> HandleAsync(LoginRequest request)
		{
			return await Email.Create(request.Email)
								.Bind(email => _authService.Login(email, request.Password))
								.OnFailure(() => SetStatusCode(HttpStatusCode.BadRequest))
								.Finally(result => result.IsSuccess ?
									ApiResult.Success(new LoginResponse(result.Value.AccessToken,
													result.Value.ExpiresIn,
													result.Value.UserToken.Email,
													result.Value.UserToken.Roles)) :
									ApiResult.Failure<LoginResponse>(result.Error));
		}
	}
}

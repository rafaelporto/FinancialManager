using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity;
using FinancialManager.Endpoints.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using FinancialManager.Infra.ValueObjects;

namespace FinancialManager.Server.Endpoints.Authorization
{
    public class Login : BaseServerEndpoint
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
		[ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.BadRequest)]
		[AllowAnonymous]
		public async Task<LoginResponse> HandleAsync(LoginRequest request)
		{
			return await Email.Create(request.Email)
								.Bind(email => _authService.Login(email, request.Password))
								.Finally(result => result.IsSuccess ?
									LoginResponse.Success(result.Value.AccessToken,
													result.Value.ExpiresIn,
													result.Value.UserToken.Email,
													result.Value.UserToken.Roles) :
									LoginResponse.Failure(result.Error));
		}
	}
}

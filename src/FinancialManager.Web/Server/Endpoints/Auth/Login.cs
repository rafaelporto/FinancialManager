using System.Threading.Tasks;
using AutoMapper;
using FinancialManager.Identity;
using FinancialManager.Notifications;
using FinancialManager.Web.Shared.Endpoints;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using FinancialManager.Web.Shared.Models;
using FinancialManager.Identity.Jwt;

namespace FinancialManager.Web.Server.Endpoints.Auth
{
	public class Login : BaseServerEndpoint
	{
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public Login(IAuthService authService,
						IMapper mapper,
						INotificationContext notifications) : base(notifications)
		{
			_authService = authService;
			_mapper = mapper;
		}

		public async Task<IActionResult> HandleAsync(LoginRequest request)
		{
			return await _authService.Login(request.Email, request.Password)
				.Finally<UserResponse, IActionResult>(rest => rest.IsSuccess ?
								Ok(LoginResponse.Success(rest.Value.AccessToken,
												rest.Value.ExpiresIn,
												rest.Value.UserToken.Email,
												_mapper.Map<Claim[]>(rest.Value.UserToken.Claims))) :
								BadRequest(LoginResponse.Failure(_notifications.AsDictionary)));
		}
	}
}

using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Core.Bases;
using FinancialManager.Identity.Jwt;
using FinancialManager.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FinancialManager.Identity
{
	internal class AuthService : BaseService, IAuthService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppJwtSettings _jwtSettings;
		private readonly JwtBuilder _jwtBuilder;

		public AuthService(SignInManager<ApplicationUser> signInManager,
								UserManager<ApplicationUser> userManager,
								IOptions<AppJwtSettings> options,
								INotificationContext notificationContext) : base(notificationContext)
		{
			_jwtSettings = options?.Value ?? throw new ArgumentNullException("Jwt settings must be set.");
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtBuilder = new JwtBuilder(_userManager, _jwtSettings);
		}

		public async Task<Result<UserResponse>> Login(string email, string password)
		{
			if (email is null or "")
			{
				_notificationContext.AddNotification("Login", "Email can't be null or empty.");
				return Result.Failure<UserResponse>("Email is required.");
			}

			if (password is null or "")
			{
				_notificationContext.AddNotification("Login", "Password can't be null or empty.");
				return Result.Failure<UserResponse>("Password is required.");
			}

			var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

			if (!result.Succeeded)
			{
				_notificationContext.AddNotification("Login", "Username or password are not correct.");
				return Result.Failure<UserResponse>("Username or password are not correct.");
			}

			var userResponse = _jwtBuilder.WithEmail(email)
										   .WithJwtClaims()
										   .WithUserRoles()
										   .BuildUserResponse();

			return Result.Success(userResponse);
		}
	}
}

using FinancialManager.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Infrastructure.Identity
{
    public interface IAuthService
    {
		Task<TokenResponse> Login(LoginRequest request, CancellationToken cancellationToken);
		Task<bool> Register(RegisterRequest request, CancellationToken cancellationToken);
	}
    internal class AuthService : IAuthService
    {

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IJwtBuilder _jwtBuilder;
		private readonly JwtSettings _jwtSettings;
		private readonly IScopeControl _scopeControl;

		public AuthService(UserManager<ApplicationUser> userManager,
							SignInManager<ApplicationUser> signInManager,
							IOptions<JwtSettings> options,
							IScopeControl scopeControl)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtBuilder = JwtBuilder.CreateBuilder();
			_jwtSettings = options.Value;
			_scopeControl = scopeControl;
		}

		public async Task<TokenResponse> Login(LoginRequest request, CancellationToken cancellationToken)
		{
			if (request.IsInValid)
			{
				_scopeControl.AddNotifications(request.Notifications);
				return default;
			}

			var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

			if (!result.Succeeded)
			{
				Notification loginNotification = new("Login", "Username or password are not correct.");
				_scopeControl.AddNotification(loginNotification);
				return default;
			}

			var tokenResponse = _jwtBuilder.WithUserManager(_userManager)
												.WithJwtSettings(_jwtSettings)
												.WithEmail(request.Email)
												.WithJwtClaims()
												.WithUserClaims()
												.WithUserRoles()
												.BuildToken()
												.GetTokenResponse();
			return tokenResponse;
		}

		public async Task<bool> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			if (request.IsInValid)
			{
				_scopeControl.AddNotifications(request.Notifications);
				return false;
			}

			var newUser = new ApplicationUser(request.FirstName, request.LastName, request.Email.Value);

			var result = await _userManager.CreateAsync(newUser, request.Password);

			if (result.Succeeded)
				return true;

			var createNotifications = result.Errors.MapToNotification();
			_scopeControl.AddNotifications(createNotifications);

			return false;
		}
	}
}

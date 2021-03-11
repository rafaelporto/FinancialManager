using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity.Jwt;
using FinancialManager.Infra.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FinancialManager.Identity
{
	internal class AuthService : IAuthService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppJwtSettings _jwtSettings;
		private readonly JwtBuilder _jwtBuilder;

		public AuthService(SignInManager<ApplicationUser> signInManager,
								UserManager<ApplicationUser> userManager,
								IOptions<AppJwtSettings> options)
		{
			_jwtSettings = options?.Value ?? throw new ArgumentNullException("Jwt settings must be set.");
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtBuilder = new JwtBuilder(_userManager, _jwtSettings);
		}

		public async Task<Result<UserResponse>> Login(Email email, string password)
		{
			if (password is null or "")
				return Result.Failure<UserResponse>("Password can't be null or empty.");
			
			var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

			if (!result.Succeeded)
				return Result.Failure<UserResponse>("Username or password are not correct.");

			var userResponse = _jwtBuilder.WithEmail(email)
										   .WithJwtClaims()
										   .WithUserRoles()
										   .BuildUserResponse();

			return Result.Success(userResponse);
		}
	}
}

using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Identity
{
    internal class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(UserManager<ApplicationUser> userManager) =>
			_userManager = userManager;
	}
}

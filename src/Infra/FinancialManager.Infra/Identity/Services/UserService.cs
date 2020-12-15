using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;

namespace FinancialManager.Identity
{
	internal class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly INotificationContext _notificationContext;

		public UserService(UserManager<ApplicationUser> userManager,
							INotificationContext notificationContext)
		{
			_userManager = userManager;
			_notificationContext = notificationContext;
		}
	}
}

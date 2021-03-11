using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using FinancialManager.Identity;
using FinancialManager.Shared.Models;
using Microsoft.AspNetCore.Authentication;

namespace FinancialManager.Server.Endpoints
{
    public abstract class BaseServerEndpoint : BaseAsyncEndpoint
	{
		protected async Task<UserInfo> CurrentUserInfo()
		{
			var userInfo = UserInfo.Anonymous;
			if (User.Identity.IsAuthenticated)
			{
				userInfo.AccessToken = await HttpContext.GetTokenAsync("access_token");
				userInfo.Email = User.GetEmail();
				userInfo.IsAuthenticated = true;
				userInfo.Roles = User.GetRoles();
				userInfo.Name = User.GetName();
			}

			return userInfo;
		}
	}
}

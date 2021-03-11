using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FinancialManager.Identity;
using FinancialManager.Endpoints.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinancialManager.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinancialManager.Server.Endpoints.Authorization
{
    public class User : BaseServerEndpoint
	{
		[HttpGet("auth/user")]
		[SwaggerOperation(
			Summary = "User",
			Description = "Get User info",
			OperationId = "auth.user",
			Tags = new[] { "AuthEndpoints" })
		]
		[ProducesResponseType(typeof(UserInfo), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[Authorize]
		public async Task<UserInfo> HandleAsync()
		
		{
			return User.Identity.IsAuthenticated ? 
					await CurrentUserInfo() :
					UserInfo.Anonymous;
		}
	}
}

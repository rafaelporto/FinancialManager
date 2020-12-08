using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using FinancialManager.Web.Shared.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinancialManager.Web.Server.Endpoints.Auth
{
	public class Register : BaseAsyncEndpoint<RegisterUserRequest, RegisterUserResponse>
	{
		[HttpPost("user")]
		[ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.BadRequest)]
		[SwaggerOperation(
			Summary = "Register a user",
			Description = "This endpoint is for a new user register yourself",
			OperationId = "user.register",
			Tags = new[] { "UserEndpoints" })
		]
		public override async Task<ActionResult<RegisterUserResponse>> HandleAsync(RegisterUserRequest request,
			CancellationToken cancellationToken = default)
		{
			return Ok(new RegisterUserResponse()
			{
				IsSuccess = true
			});
		}
	}
}

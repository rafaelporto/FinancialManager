using FinancialManager.Core;
using FinancialManager.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace FinancialManager.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ControllerBase : Controller
    {
        protected IScopeControl ScopeControl { get; }

        public ControllerBase(IScopeControl scopeControl)
        {
            ScopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl));
        }

        protected IActionResult ApiResponse<T>(T result)
        {
            if (ScopeControl.HasNotification)
            {
                var notifications = ScopeControl.Notifications.MapToDictionary();
                return BadRequest(ApiResult.Failure<T>(notifications));
            }

            return Ok(ApiResult.Success(result));
        }

        protected IActionResult ApiResponse(HttpStatusCode successCode = HttpStatusCode.OK)
        {
            if (ScopeControl.HasNotification)
            {
                var notifications = ScopeControl.Notifications.MapToDictionary();
                return BadRequest(ApiResult.Failure(notifications));
            }

            return new JsonResult(ApiResult.Success()) { StatusCode = (int)successCode };
        }

        protected IActionResult ApiCreatedResponse() => ApiResponse(HttpStatusCode.Created);

        protected IActionResult ApiNoContentResponse()
        {
            if (ScopeControl.HasNotification)
            {
                var notifications = ScopeControl.Notifications.MapToDictionary();
                return BadRequest(ApiResult.Failure(notifications));
            }

            return NoContent();
        }
    }
}

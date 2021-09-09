using FinancialManager.Core;
using FinancialManager.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace FinancialManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ManagerController : ControllerBase
    {
        private readonly NotificationHandler _notifications;

        public ManagerController(INotificationHandler<Notification> notifications)
        {
            if (notifications is null)
                throw new ArgumentNullException(nameof(notifications));

            _notifications = notifications as NotificationHandler;
        }

        protected IActionResult ApiResponse<T>(T result)
        {
            if (_notifications.HasNotification())
            {
                var notifications = _notifications.GetNotifications().MapToDictionary();
                return BadRequest(ApiResult.Failure<T>(notifications));
            }

            return Ok(ApiResult.Success(result));
        }

        protected IActionResult ApiResponse(HttpStatusCode successCode = HttpStatusCode.OK)
        {
            if (_notifications.HasNotification())
            {
                var notifications = _notifications.GetNotifications().MapToDictionary();
                return BadRequest(ApiResult.Failure(notifications));
            }

            return new JsonResult(ApiResult.Success()) { StatusCode = (int)successCode };
        }
    }
}

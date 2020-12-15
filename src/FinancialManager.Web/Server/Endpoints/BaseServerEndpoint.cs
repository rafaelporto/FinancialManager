using Ardalis.ApiEndpoints;
using FinancialManager.Notifications;

namespace FinancialManager.Web.Server.Endpoints
{
	public abstract class BaseServerEndpoint : BaseAsyncEndpoint
	{
		protected readonly INotificationContext _notifications;

		public BaseServerEndpoint(INotificationContext notifications) =>
			_notifications = notifications;
	}
}

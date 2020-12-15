using FinancialManager.Notifications;

namespace FinancialManager.Core.Bases
{
	public abstract class BaseService
	{
		protected readonly INotificationContext _notificationContext;

		public BaseService(INotificationContext notificationContext) =>
			_notificationContext = notificationContext;
		
	}
}

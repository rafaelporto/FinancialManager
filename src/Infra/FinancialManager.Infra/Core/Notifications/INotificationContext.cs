using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace FinancialManager.Notifications
{
	public interface INotificationContext
	{
		IReadOnlyCollection<Notification> Notifications { get; }
		IDictionary<string, string> AsDictionary { get; }
		IReadOnlyCollection<string> Messages { get; }
		Result AddNotification(string code, string message);
		Result AddNotification(Notification notification);
		Result AddNotifications(IEnumerable<(string code, string message)> notifications);
		Result AddNotifications(Notification[] notifications);
		Result AddNotifications(IEnumerable<ValidationFailure> errors);
		public bool HasNotification();
		public bool HasNoNotification();
	}
}

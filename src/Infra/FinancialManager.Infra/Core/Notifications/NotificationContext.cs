using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace FinancialManager.Notifications
{
	internal class NotificationContext : INotificationContext
	{
		private readonly List<Notification> _notifications = new List<Notification>();
		public IDictionary<string, string> AsDictionary => _notifications.ToDictionary(p => p.Code, p => p.Message);

		public IReadOnlyCollection<Notification> Notifications => _notifications;
		public IReadOnlyCollection<string> Messages => _notifications?.Select(p => p.Message)?.ToArray() ?? Array.Empty<string>();

		public bool HasNotification() => _notifications.Any();
		public bool HasNoNotification() => !HasNotification();

		public Result AddNotification(string key, string message) =>
			Notification.Create(key, message)
						.Check(notification =>
						{
							_notifications.Add(notification);
							return Result.Success();
						});


		public Result AddNotification(Notification notification)
		{
			_notifications.Add(notification);
			return Result.Success();
		}
		public Result AddNotifications(IEnumerable<(string code, string message)> notifications)
		{
			var result = notifications.Select(s => Notification.Create(s.code, s.message));

			if (result.Any(p => p.IsFailure))
				return Result.Failure("Was not possible create the notifications.");

			_notifications.AddRange(result.Select(p => p.Value));

			return Result.Success();
		}

		public Result AddNotifications(Notification[] notifications)
		{
			_notifications.AddRange(notifications);
			return Result.Success();
		}

		public Result AddNotifications(IEnumerable<ValidationFailure> errors)
		{
			_notifications.AddRange(errors.Select(error => Notification.Create(error)));
			return Result.Success();
		}
	}
}

using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace FinancialManager.Notifications
{
	public record Notification
	{
		public string Code { get; }
		public string Message { get; }

		private Notification(string key, string message) =>
			(Code, Message) = (key, message);

		public static Result<Notification> Create(string key, string message)
		{
			if (key is null or "")
				return Result.Failure<Notification>("Key notification is required.");

			if (message is null or "")
				return Result.Failure<Notification>("Message notification is required.");

			return Result.Success(new Notification(key, message));
		}

		public static Notification Create(ValidationFailure validationFailure) =>
			new Notification(validationFailure.PropertyName, validationFailure.ErrorMessage);

		public static implicit operator KeyValuePair<string, string>(Notification value) =>
			KeyValuePair.Create(value.Code, value.Message);
	}
}
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace FinancialManager.Endpoints.Authorization
{
    public record LoginResponse
	{
		public string AccessToken { get; }
		public DateTimeOffset ExpiresIn { get; }
		public string Email { get; }
		public IReadOnlyList<string> Roles { get; }
		public IReadOnlyList<string> Notifications { get; }
		public bool IsSuccessed => Notifications is null || Notifications.Count == 0;
		public bool IsFailure => IsSuccessed is false;

		[JsonConstructor]
		public LoginResponse(string accessToken, DateTimeOffset expiresIn, string email, IReadOnlyList<string> roles, IReadOnlyList<string> notifications)
		{
			AccessToken = accessToken;
			ExpiresIn = expiresIn;
			Email = email;
			Roles = roles;
			Notifications = notifications;
		}

		private LoginResponse(string[] notifications) => Notifications = notifications;

		public static LoginResponse Success(string accessToken,
											DateTimeOffset expiresIn,
											string email,
											IReadOnlyList<string> roles) =>
			new LoginResponse(accessToken, expiresIn, email, roles, default);

		public static LoginResponse Failure(string[] notifications) =>
			new LoginResponse(notifications);

		public static LoginResponse Failure(string error) =>
			new LoginResponse(new string[] { error });
	}
}

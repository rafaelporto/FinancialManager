using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinancialManager.Endpoints.Authorization
{
    public class LoginResponse
	{
		public string AccessToken { get; }
		public DateTimeOffset ExpiresIn { get; }
		public string Email { get; }
		public IReadOnlyList<string> Roles { get; }

		[JsonConstructor]
		public LoginResponse(string accessToken, DateTimeOffset expiresIn, string email, IReadOnlyList<string> roles)
		{
			AccessToken = accessToken;
			ExpiresIn = expiresIn;
			Email = email;
			Roles = roles;
		}
	}
}

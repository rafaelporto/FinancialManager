using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinancialManager.Api
{
    public record LoginResponseModel
	{
		public string AccessToken { get; }
		public DateTimeOffset ExpiresIn { get; }
		public string Email { get; }
		public IReadOnlyList<string> Roles { get; }

		[JsonConstructor]
		public LoginResponseModel(string accessToken, DateTimeOffset expiresIn, string email, IReadOnlyList<string> roles)
		{
			AccessToken = accessToken;
			ExpiresIn = expiresIn;
			Email = email;
			Roles = roles;
		}
	}
}

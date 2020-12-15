using System.Collections.Generic;
using FinancialManager.Web.Shared.Models;

namespace FinancialManager.Web.Shared.Endpoints
{
	public record LoginResponse
	{
		public string AccessToken { get; }
		public double ExpiresIn { get; }
		public string Email { get; }
		public IEnumerable<Claim> Claims { get; }
		public IReadOnlyDictionary<string, string> Notifications { get; set; }

		private LoginResponse(string accessToken, double expiresIn, string email, IEnumerable<Claim> claims)
		{
			AccessToken = accessToken;
			ExpiresIn = expiresIn;
			Email = email;
			Claims = claims;
		}

		private LoginResponse(Dictionary<string, string> notifications) =>
			Notifications = notifications;

		public static LoginResponse Success(string accessToken,
											double expiresIn,
											string email,
											IEnumerable<Claim> claims) =>
			new LoginResponse(accessToken, expiresIn, email, claims);

		public static LoginResponse Failure(IDictionary<string, string> notifications) =>
			new LoginResponse(notifications as Dictionary<string, string>);

	}
}

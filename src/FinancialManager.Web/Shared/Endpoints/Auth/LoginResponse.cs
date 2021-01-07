using System.Collections.Generic;

namespace FinancialManager.Web.Shared.Endpoints
{
	public record LoginResponse
	{
		public string AccessToken { get; }
		public double ExpiresIn { get; }
		public string Email { get; }
		public IReadOnlyList<string> Roles { get; }
		public IReadOnlyList<string> Notifications { get; set; }

		private LoginResponse(string accessToken, double expiresIn, string email, IList<string> roles)
		{
			AccessToken = accessToken;
			ExpiresIn = expiresIn;
			Email = email;
			Roles = roles as IReadOnlyList<string>;
		}

		private LoginResponse(IEnumerable<string> notifications) =>
			Notifications = notifications as IReadOnlyList<string>;

		public static LoginResponse Success(string accessToken,
											double expiresIn,
											string email,
											IList<string> roles) =>
			new LoginResponse(accessToken, expiresIn, email, roles);

		public static LoginResponse Failure(IEnumerable<string> notifications) =>
			new LoginResponse(notifications);

	}
}

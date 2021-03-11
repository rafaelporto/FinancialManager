using System;

namespace FinancialManager.Identity.Jwt
{
	public class UserResponse
    {
        public string AccessToken { get; set; }
        public DateTimeOffset ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}

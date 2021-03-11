using System;
using System.Collections.Generic;

namespace FinancialManager.Identity.Jwt
{
	public class UserToken
    {
        public string Email { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
    }
}

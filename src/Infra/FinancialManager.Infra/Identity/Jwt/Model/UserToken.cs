using System;
using System.Collections.Generic;

namespace FinancialManager.Identity.Jwt
{
	public class UserToken
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; init; }
		public string LastName { get; init; }
	}
}

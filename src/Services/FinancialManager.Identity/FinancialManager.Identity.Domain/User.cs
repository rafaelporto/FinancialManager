using FinancialManager.Core.DomainObjects;
using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Identity.Domain
{
	public class User : IdentityUser, IAggregateRoot
	{
		public string Name { get; init; }
		public string LastName { get; init; }
	}
}

using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Identity
{
	internal partial class UserStore<TUser, TRole, TDocumentStore> : IQueryableUserStore<TUser>
	{
		public IQueryable<TUser> Users => Session.Query<TUser>();
	}
}

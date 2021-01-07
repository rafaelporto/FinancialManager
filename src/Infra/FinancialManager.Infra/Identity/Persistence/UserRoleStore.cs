using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;

namespace FinancialManager.Identity
{
	internal partial class UserStore<TUser, TRole, TDocumentStore> : IUserRoleStore<TUser>
	{
		public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			var roleStored = await Session.LoadAsync<IdentityRole>(BuildRoleId(roleName), cancellationToken);

			if (roleStored is null)
			{
				roleStored = new TRole { Name = roleName };
				await Session.StoreAsync(roleStored, BuildRoleId(roleName), cancellationToken);
			}

			if (!user.Roles.Contains(roleStored.Name, StringComparer.InvariantCultureIgnoreCase))
				user.GetRolesList().Add(roleStored.Name);
		}

		public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			return Task.FromResult<IList<string>>(new List<string>(user.Roles));
		}

		public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (roleName is null or "")
				throw new ArgumentNullException(nameof(roleName));

			return await Session.Query<TUser>()
				.Where(u => u.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase))
				.ToListAsync(cancellationToken);
		}

		public Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (roleName is null or "")
				throw new ArgumentNullException(nameof(roleName));

			return Task.FromResult(user.Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase));
		}

		public Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (roleName is not null or "")
				user.GetRolesList().RemoveAll(role => string.Equals(role, roleName, StringComparison.InvariantCultureIgnoreCase));

			return Task.CompletedTask;
		}
	}
}

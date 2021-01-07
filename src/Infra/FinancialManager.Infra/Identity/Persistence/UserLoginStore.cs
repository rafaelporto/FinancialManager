using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;

namespace FinancialManager.Identity
{
	internal partial class UserStore<TUser, TRole, TDocumentStore> : IUserLoginStore<TUser>
	{
		public Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
		{
			ThrowIfDisposed();
			if (login is null)
				throw new ArgumentNullException(nameof(login));

			user.Logins.Add(login);
			return Task.CompletedTask;
		}

		public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			return Session.Query<TUser>()
				.FirstOrDefaultAsync(u => 
					u.Logins.Any(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey),
					cancellationToken);
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			return Task.FromResult(user.Logins as IList<UserLoginInfo>);
		}

		public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			user.Logins.RemoveAll(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
			return Task.CompletedTask;
		}
	}
}

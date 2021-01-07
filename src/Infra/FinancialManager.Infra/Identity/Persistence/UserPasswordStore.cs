using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;

namespace FinancialManager.Identity
{
	internal partial class UserStore<TUser, TRole, TDocumentStore> : IUserPasswordStore<TUser>
	{
		public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			return Task.FromResult(user.PasswordHash);
		}
		
		public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			return Task.FromResult(user.PasswordHash is not null or "");
		}

		public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user is null)
				throw new ArgumentNullException(nameof(user));

			user.PasswordHash = passwordHash;

			return Task.CompletedTask;
		}
	}
}

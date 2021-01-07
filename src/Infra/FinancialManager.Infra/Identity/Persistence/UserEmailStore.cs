using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;

namespace FinancialManager.Identity
{
	internal partial class UserStore<TUser, TRole, TDocumentStore> : IUserEmailStore<TUser>
	{
		public async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			return await Session.Query<TUser>()
								.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
		}

		public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user is null)
				throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user is null)
				throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.EmailConfirmed);
		}

		public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user == null)
				throw new ArgumentNullException(nameof(user));
			
			return Task.FromResult(user.NormalizedEmail);
		}

		public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user is null)
				throw new ArgumentNullException(nameof(user));
			
			user.Email = email;
			return Task.CompletedTask;
		}

		public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user is null)
				throw new ArgumentNullException(nameof(user));
			
			user.EmailConfirmed = confirmed;
			return Task.CompletedTask;
		}

		public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user is null)
				throw new ArgumentNullException(nameof(user));

			user.NormalizedEmail = normalizedEmail;
			return Task.CompletedTask;
		}
	}
}

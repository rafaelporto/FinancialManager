using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;
using Raven.Client.Exceptions;

namespace FinancialManager.Identity
{
	internal partial class UserStore<TUser, TRole, TDocumentStore> 
        : StoreBase<TRole, TDocumentStore>, IUserStore<TUser>
        where TUser : ApplicationUser
        where TRole : IdentityRole, new()
        where TDocumentStore: class, IDocumentStore
    {
        public IdentityErrorDescriber ErrorDescriber { get; }

		public UserStore(TDocumentStore context, IdentityErrorDescriber errorDescriber = null)
            : base(context) => ErrorDescriber = errorDescriber;

        public Task SaveChanges(CancellationToken cancellationToken = default) =>
            Session.SaveChangesAsync(cancellationToken);

        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default )
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            await Session.StoreAsync(user, cancellationToken);
            await SaveChanges(cancellationToken);

            return IdentityResult.Success;
        }

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            cancellationToken.ThrowIfCancellationRequested();
            return Session.LoadAsync<TUser>(userId, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var stored = await Session.LoadAsync<TUser>(user.Id, cancellationToken);

            await Session.StoreAsync(user, cancellationToken);

            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (ConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            Session.Delete(user.Id);

            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (ConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }

            return IdentityResult.Success;
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Session.Query<TUser>().FirstOrDefaultAsync(
                u => u.NormalizedUserName == normalizedUserName, cancellationToken
            );
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (userName is null)
                throw new ArgumentNullException(nameof(userName));

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (normalizedName is null)
                throw new ArgumentNullException(nameof(normalizedName));

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }
    }
}

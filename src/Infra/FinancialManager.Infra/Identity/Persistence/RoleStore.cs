using FinancialManager.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Identity
{
    internal class RoleStore<TRole, TDocumentStore> : StoreBase<TRole, TDocumentStore>, IQueryableRoleStore<TRole>
        where TRole : IdentityRole, new()
        where TDocumentStore : class, IDocumentStore
    {
        public IdentityErrorDescriber ErrorDescriber { get; }

		public IQueryable<TRole> Roles => Session.Query<TRole>();

		public RoleStore(TDocumentStore context, IdentityErrorDescriber errorDescriber = null)
            : base(context) => ErrorDescriber = errorDescriber;

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            cancellationToken.ThrowIfCancellationRequested();

            await Session.StoreAsync(role, BuildRoleId(role.Name), cancellationToken);
            await SaveChanges(cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            Session.Delete(role.Id);

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

        public Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            cancellationToken.ThrowIfCancellationRequested();
            return Session.LoadAsync<TRole>(roleId, cancellationToken);
        }

        public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Session.Query<TRole>().FirstOrDefaultAsync(role => 
                role.NormalizedName == normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            if (normalizedName is null)
                throw new ArgumentNullException(nameof(normalizedName));

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            if (role is null)
                throw new ArgumentNullException(nameof(roleName));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            var stored = await Session.LoadAsync<TRole>(role.Id, cancellationToken);

            await Session.StoreAsync(role, cancellationToken);

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

        public Task SaveChanges(CancellationToken cancellationToken = default) =>
           Session.SaveChangesAsync(cancellationToken);
    }
}

using System;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace FinancialManager.Identity
{
	internal abstract class StoreBase<TRole, TDocumentStore> : IDisposable
		where TRole : IdentityRole, new()
		where TDocumentStore : class, IDocumentStore
	{
		public TDocumentStore Context { get; }

		private readonly Lazy<IAsyncDocumentSession> _session;
		public IAsyncDocumentSession Session => _session.Value;

		protected StoreBase(TDocumentStore context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));

			_session = new Lazy<IAsyncDocumentSession>(() =>
			{
				var session = Context.OpenAsyncSession();
				session.Advanced.UseOptimisticConcurrency = true;
				return session;
			}, true);
		}

		protected string BuildRoleId(string roleName) =>
			Context.Conventions.GetCollectionName(typeof(TRole)) +
			Context.Conventions.IdentityPartsSeparator +
			roleName;

		#region IDisposable

		internal protected void ThrowIfDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(GetType().Name);
		}

		private bool _disposed;
		public void Dispose()
		{
			Session.Dispose();
			_disposed = true;
		}
		#endregion
	}
}

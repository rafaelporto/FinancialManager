using System;
using FinancialManager.Core.DomainObjects;

namespace FinancialManager.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

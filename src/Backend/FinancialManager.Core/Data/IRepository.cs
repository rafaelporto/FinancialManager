using FinancialManager.Core.DomainObjects;
using System;

namespace FinancialManager.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

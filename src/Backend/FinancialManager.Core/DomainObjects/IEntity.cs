using System;

namespace FinancialManager.Core.DomainObjects
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTimeOffset Created { get; }
        DateTimeOffset? LastUpdated { get; }
        bool IsDeleted { get; }
        Guid TenantId { get; }
    }
}

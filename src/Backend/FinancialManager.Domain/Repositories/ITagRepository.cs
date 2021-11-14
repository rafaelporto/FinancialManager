using FinancialManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Domain
{
    public interface ITagRepository : IRepository<Tag>
    {
        void Add(Tag tag);
        void Update(Tag tag);
        ValueTask<bool> Remove(Guid id);
        Task<Tag> Get(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetList(CancellationToken cancellationToken = default);
    }
}

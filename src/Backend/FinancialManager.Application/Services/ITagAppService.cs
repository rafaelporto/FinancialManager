using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Application
{
    public interface ITagAppService
    {
        Task<bool> Create(CreateTagModel model, CancellationToken token = default);
        Task<bool> Delete(Guid id, CancellationToken token = default);
        Task<IEnumerable<TagModel>> Get(CancellationToken token = default);
        Task<TagModel> Get(Guid id, CancellationToken token = default);
        Task<bool> Update(Guid id, TagModel model, CancellationToken token = default);
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit(CancellationToken token = default);
    }
}

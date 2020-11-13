using System.Threading.Tasks;

namespace FinancialManager.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
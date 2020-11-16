using System.Threading.Tasks;
using FinancialManager.Core;
using FinancialManager.Core.Data;

namespace FinancialManager.Identity.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Result<User>> GetUser(); 
    }
}
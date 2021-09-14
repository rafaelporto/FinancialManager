using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.FinancialAccounts.Application
{
    public interface IAccountAppService
    {
        Task<bool> CreateAccount(RegisterAccountModel model, CancellationToken token = default);
        Task<bool> DeleteAccount(Guid id, CancellationToken token = default);
        Task<IEnumerable<AccountModel>> GetAccounts(CancellationToken token = default);
        Task<AccountModel> GetAccount(Guid id, CancellationToken token = default);
        Task<bool> UpdateAccount(Guid id, EditAccountModel model, CancellationToken token = default);
    }
}

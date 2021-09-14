using FinancialManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.FinancialAccounts.Domain
{
    public interface IAccountRepository : IRepository<Account>
    {
        void Add(Account account);
        Task<bool> Remove(Guid id);
        Task<IEnumerable<Account>> GetList(CancellationToken token = default);
        Task<Account> Get(Guid id, CancellationToken token = default);
        void Update(Account account);
    }
}

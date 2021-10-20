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
        void Add(Expense expense);
        Task<bool> Remove(Guid id);
        Task<bool> RemoveExpense(Guid accountId, Guid id);
        Task<IEnumerable<Account>> GetList(CancellationToken token = default);
        Task<IEnumerable<Expense>> GetExpenses(Guid accountId, CancellationToken token = default);
        Task<Account> Get(Guid id, CancellationToken token = default);
        Task<Expense> GetExpense(Guid accountId, Guid id, CancellationToken token = default);
        void Update(Account account);
        void Update(Expense expense);
    }
}

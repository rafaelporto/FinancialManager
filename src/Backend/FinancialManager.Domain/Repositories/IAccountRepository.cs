using FinancialManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Domain
{
    public interface IAccountRepository : IRepository<FinancialAccount>
    {
        void Add(FinancialAccount account);
        void Add(Expense expense);
        Task<bool> Remove(Guid id);
        Task<bool> RemoveExpense(Guid accountId, Guid id);
        Task<IEnumerable<FinancialAccount>> GetList(CancellationToken token = default);
        Task<IEnumerable<Expense>> GetExpenses(Guid accountId, CancellationToken token = default);
        Task<FinancialAccount> Get(Guid id, CancellationToken token = default);
        Task<Expense> GetExpense(Guid accountId, Guid id, CancellationToken token = default);
        void Update(FinancialAccount account);
        void Update(Expense expense);
    }
}

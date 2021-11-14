using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Application
{
    public interface IAccountAppService
    {
        Task<bool> CreateAccount(RegisterAccountModel model, CancellationToken token = default);
        Task<bool> DeleteAccount(Guid id, CancellationToken token = default);
        Task<IEnumerable<AccountModel>> GetAccounts(CancellationToken token = default);
        Task<AccountModel> GetAccount(Guid id, CancellationToken token = default);
        Task<bool> UpdateAccount(Guid id, EditAccountModel model, CancellationToken token = default);

        #region Expenses
        Task<bool> CreateExpense(Guid accountId, CreateExpenseModel model, CancellationToken token = default);
        Task<bool> DeleteExpense(Guid accountId, Guid id, CancellationToken token = default);
        Task<IEnumerable<ExpenseModel>> GetExpenses(Guid accountId, CancellationToken token = default);
        Task<ExpenseModel> GetExpense(Guid accountId, Guid id, CancellationToken token = default);
        Task<bool> UpdateExpense(Guid accountId, Guid id, EditExpenseModel model, CancellationToken token = default);
        Task AddTagToExpense(Guid accountId, Guid id, Guid[] tagsIds, CancellationToken token = default);
        #endregion
    }
}

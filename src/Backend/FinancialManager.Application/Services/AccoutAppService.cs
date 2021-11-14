using FinancialManager.Core;
using FinancialManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Application
{
    public class AccoutAppService : IAccountAppService
    {
        private readonly IAccountRepository _repository;
        private readonly ITagRepository _tagRepository;
        private readonly IScopeControl _scopeControl;

        public AccoutAppService(IAccountRepository repository, ITagRepository tagRepository, IScopeControl scopeControl)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _scopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl)); ;
        }

        public async Task<bool> CreateAccount(RegisterAccountModel model, CancellationToken token = default)
        {
            var account = model.MapToAccount(_scopeControl.GetUserId());

            if (account.IsInValid())
            { 
                _scopeControl.AddNotifications(account.Notifications);
                return false;
            }

            if (token.IsCancellationRequested)
                return false;

            _repository.Add(account);
            return await _repository.UnitOfWork.Commit(token);
        }

        public async Task<AccountModel> GetAccount(Guid id, CancellationToken token = default)
        {
            var account = await _repository.Get(id, token);
            return account.MapToAccountModel();
        }

        public async Task<IEnumerable<AccountModel>> GetAccounts(CancellationToken token = default)
        {
            var accounts = await _repository.GetList(token);

            return accounts.MapToAccountModels();
        }

        public async Task<bool> UpdateAccount(Guid id, EditAccountModel model, CancellationToken token = default)
        {
            var account = model.MapToAccount(id, _scopeControl.GetUserId());

            if (account.IsInValid())
            {
                _scopeControl.AddNotifications(account.Notifications);
                return false;
            }

            _repository.Update(account);
            return await _repository.UnitOfWork.Commit(token);
        }

        public async Task<bool> DeleteAccount(Guid id, CancellationToken token = default)
        {
            if (await _repository.Remove(id))
            {
                return await _repository.UnitOfWork.Commit(token);
            }

            return false;
        }

        #region Expenses
        public async Task<bool> CreateExpense(Guid accountId, CreateExpenseModel model, CancellationToken token = default)
        {
            var expense = model.MapToExpense(accountId, _scopeControl.GetUserId());

            if (expense.IsInValid())
            {
                _scopeControl.AddNotifications(expense.Notifications);
                return false;
            }

            if (token.IsCancellationRequested)
                return false;

            _repository.Add(expense);
            return await _repository.UnitOfWork.Commit(token);
        }

        public async Task<bool> DeleteExpense(Guid accountId, Guid id, CancellationToken token = default)
        {
            if (await _repository.RemoveExpense(accountId, id))
                return await _repository.UnitOfWork.Commit(token);

            return false;
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpenses(Guid accountId, CancellationToken token = default)
        {
            var expenses = await _repository.GetExpenses(accountId, token);

            return expenses.MapToExpenseModels();
        }

        public async Task<ExpenseModel> GetExpense(Guid accountId, Guid id, CancellationToken token = default)
        {
            var expense = await _repository.GetExpense(accountId, id, token);

            return expense.MapToExpenseModel();
        }

        public async Task<bool> UpdateExpense(Guid accountId, Guid id, EditExpenseModel model, CancellationToken token = default)
        {
            var expense = model.MapToExpense(id, accountId, _scopeControl.GetUserId());

            if (expense.IsInValid())
            {
                _scopeControl.AddNotifications(expense.Notifications);
                return false;
            }

            _repository.Update(expense);
            return await _repository.UnitOfWork.Commit(token);
        }

        public async Task AddTagToExpense(Guid accountId, Guid id, Guid[] tagsIds, CancellationToken token = default)
        {
            if (tagsIds != default && tagsIds.Length > 0)
            {
                var allTags = await _tagRepository.GetList(token);

                var tagsToAdd = allTags.Where(p => tagsIds.Any(x => x == p.Id));
                var expense = await _repository.GetExpense(accountId, id, token);

                foreach (var tagToAdd in tagsToAdd)
                {
                    if (!expense.Tags.Any(p => p.Id == tagToAdd.Id))
                        expense.Tags.Add(tagToAdd);
                }

                _repository.Update(expense);
                await _repository.UnitOfWork.Commit(token);
            }

            else
                _scopeControl.AddNotification(new("tagsIds", "Tag ids are required."));
        }
        #endregion
    }
}

using FinancialManager.Core.Data;
using FinancialManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Data
{
    public class FinancialAccountRepository : IAccountRepository
    {
        private readonly FinancialManagerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public FinancialAccountRepository(FinancialManagerContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Dispose() => _context?.Dispose();
        public void Add(FinancialAccount account) => _context.Accounts.Add(account);
        public async Task<FinancialAccount> Get(Guid id, CancellationToken token = default) =>
             await _context.Accounts.FirstOrDefaultAsync(p => p.Id == id, token);

        public async Task<IEnumerable<FinancialAccount>> GetList(CancellationToken token = default)
        {
            var result = await _context.Accounts.AsNoTracking()
                                        .ToListAsync(token);
            return result;
        }

        public void Update(FinancialAccount account) => _context.Accounts.Update(account);

        public async Task<bool> Remove(Guid id)
        {
            var financialAccount = await _context.Accounts.FirstOrDefaultAsync(p => p.Id == id);

            if (financialAccount is null)
                return false;

            financialAccount.Delete();
            _context.Update(financialAccount);
            return true;
        }

        #region Expenses
        public void Add(Expense expense) => _context.Expenses.Add(expense);

        public async Task<bool> RemoveExpense(Guid accountId, Guid id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(p => p.Id == id && p.AccountId == accountId);

            if (expense is null)
                return false;

            expense.Delete();
            _context.Update(expense);
            return true;
        }

        public async Task<IEnumerable<Expense>> GetExpenses(Guid accountId, CancellationToken token = default)
        {
            var result = await _context.Expenses.AsNoTracking()
                                        .Include(p => p.Tags)
                                        .Where(p => p.AccountId == accountId)
                                        .ToListAsync(token);
            return result;
        }

        public async Task<Expense> GetExpense(Guid accountId, Guid id, CancellationToken token = default) =>
            await _context.Expenses.FirstOrDefaultAsync(p => p.Id == id && p.AccountId == accountId, token);

        public void Update(Expense expense) => _context.Expenses.Update(expense);
        #endregion
    }
}

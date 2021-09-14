using FinancialManager.Core;
using FinancialManager.Core.Data;
using FinancialManager.FinancialAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.FinancialAccounts.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;
        private readonly IScopeControl _scopeControl;

        public AccountRepository(AccountContext context, IScopeControl scopeControl)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _scopeControl = scopeControl ?? throw new ArgumentNullException(nameof(scopeControl));
        }

        public IUnitOfWork UnitOfWork => _context;


        public void Dispose() => _context?.Dispose();
        public void Add(Account account) => _context.Accounts.Add(account);
        public async Task<Account> Get(Guid id, CancellationToken token = default) =>
             await _context.Accounts.FirstOrDefaultAsync(p => p.Id == id, token);

        public async Task<IEnumerable<Account>> GetList(CancellationToken token = default)
        {
            var result = await _context.Accounts.AsNoTracking()
                                        .ToListAsync(token);
            return result;
        }

        public void Update(Account account) => _context.Accounts.Update(account);

        public async Task<bool> Remove(Guid id)
        {
            if (await _context.Accounts.AnyAsync(p => p.Id == id))
            {
                Account account = Account.CreateEmptyAccount(id);

                _context.Accounts.Add(account);
                _context.Entry(account).State = EntityState.Deleted;
                return true;
            }

            return false;
        }
    }
}

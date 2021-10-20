using FinancialManager.FinancialAccounts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialManager.FinancialAccounts.Application
{
    public static class AccountMapper
    {
        public static Account MapToAccount(this RegisterAccountModel model, Guid aspNetUserId) =>
            new(model.AccountName, model.AccountType, aspNetUserId);

        public static Account MapToAccount(this EditAccountModel model, Guid id, Guid ownerId) =>
            new(id, model.AccountName, model.AccountType, ownerId);

        public static AccountModel MapToAccountModel(this Account entity) =>
            new()
            { 
                AccountName = entity.AccountName,
                AccountType = entity.AccountType,
                Created = entity.Created,
                Id = entity.Id,
                LastUpdated = entity.LastUpdated,
                OwnerId = entity.TenantId
            };

        public static IEnumerable<AccountModel> MapToAccountModels(this IEnumerable<Account> model) =>
            model.Select(s => s.MapToAccountModel());
    }
}

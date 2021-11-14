using FinancialManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialManager.Application
{
    public static class AccountMapper
    {
        public static FinancialAccount MapToAccount(this RegisterAccountModel model, Guid aspNetUserId) =>
            new(model.AccountName, model.AccountType, aspNetUserId);

        public static FinancialAccount MapToAccount(this EditAccountModel model, Guid id, Guid ownerId) =>
            new(id, model.AccountName, model.AccountType, ownerId);

        public static AccountModel MapToAccountModel(this FinancialAccount entity) =>
            new()
            { 
                AccountName = entity.AccountName,
                AccountType = entity.AccountType,
                Created = entity.Created,
                Id = entity.Id,
                LastUpdated = entity.LastUpdated,
                OwnerId = entity.TenantId
            };

        public static IEnumerable<AccountModel> MapToAccountModels(this IEnumerable<FinancialAccount> model) =>
            model.Select(s => s.MapToAccountModel());
    }
}

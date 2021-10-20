using FinancialManager.FinancialAccounts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialManager.FinancialAccounts.Application
{
    public static class ExpenseMapper
    {
        public static Expense MapToExpense(this CreateExpenseModel model, Guid accountId, Guid aspNetUserId) =>
            new(model.Description, model.Amount, model.Type, model.Date, accountId, aspNetUserId);

        public static Expense MapToExpense(this EditExpenseModel model, Guid id, Guid accountId, Guid aspNetUserId) =>
            new(id, model.Description, model.Amount, model.Type, model.Date, accountId, aspNetUserId);

        public static ExpenseModel MapToExpenseModel(this Expense entity) =>
            new()
            {
                Id = entity.Id,
                Description = entity.Description,
                Amount = entity.Amount,
                Type = entity.Type,
                Date = entity.Date,
                AccountId = entity.AccountId
            };

        public static IEnumerable<ExpenseModel> MapToExpenseModels(this IEnumerable<Expense> model) =>
            model.Select(s => s.MapToExpenseModel());
    }
}

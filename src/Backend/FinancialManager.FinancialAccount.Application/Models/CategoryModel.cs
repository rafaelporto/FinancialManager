using FinancialManager.FinancialAccounts.Domain;
using System;
using System.Collections.Generic;

namespace FinancialManager.FinancialAccounts.Application
{
    public record CategoryModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ICollection<ExpenseModel> Expenses { get; set; }
    }
    public record CreateCategoryModel
    {
        public string Description { get; set; }
    }
}

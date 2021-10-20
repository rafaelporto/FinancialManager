using FinancialManager.Core;
using FinancialManager.FinancialAccounts.Domain;
using System;
using System.Collections.Generic;

namespace FinancialManager.FinancialAccounts.Application
{
    public record ExpenseModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid AccountId { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
    public record CreateExpenseModel
    {
        public string Description { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
    public record EditExpenseModel
    {
        public string Description { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
}

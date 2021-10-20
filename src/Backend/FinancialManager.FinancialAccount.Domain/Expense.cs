using FinancialManager.Core;
using FinancialManager.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace FinancialManager.FinancialAccounts.Domain
{
    public class Expense : Entity
    {
        public string Description { get; private set; }
        public ExpenseType Type { get; private set; }
        public Amount Amount { get; private set; }
        public DateTimeOffset Date { get; set; }
        public Guid AccountId { get; private set; }
        public virtual Account Account { get; private set; }
        public virtual ICollection<Category> Categories { get; private set; }
        protected override ValidationResult Validations => new ExpenseValidation().Validate(this);

        public Expense(string description, Amount amount, ExpenseType type, DateTimeOffset date, Guid accountId, Guid tenantId)
        {
            Description = description;
            Amount = amount;
            Type = type;
            Date = date;
            AccountId = accountId;
            TenantId = tenantId;
            IsValid();
        }

        public Expense(Guid id, string description, Amount amount, ExpenseType type, DateTimeOffset date, Guid accountId, Guid tenantId)
            : this(description, amount, type, date, accountId, tenantId) =>
            Id = id;

        public static Expense CreateEmpty(Guid id) => new() { Id = id };
        protected Expense() { }
    }

    internal class ExpenseValidation : AbstractValidator<Expense>
    {
        public ExpenseValidation()
        {
            RuleFor(p => p.Description)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(1)
                    .MaximumLength(50);

            RuleFor(p => p.Type).IsInEnum();

            RuleFor(p => p.AccountId).NotEmpty();
        }
    }
}

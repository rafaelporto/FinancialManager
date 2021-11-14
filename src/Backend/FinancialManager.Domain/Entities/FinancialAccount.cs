using FinancialManager.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Domain
{
    public class FinancialAccount : Entity, IAggregateRoot
    {
        public string AccountName { get; private set; }
        public AccountType AccountType { get; private set; }
        public virtual List<Expense> Expenses { get; set; }
        public virtual List<Tag> Tags { get; private set; }

        [NotMapped]
        protected override ValidationResult Validations => new AccountValidator().Validate(this);

        public FinancialAccount(Guid id, string accountName, AccountType accountType, Guid applicationUserId) 
            : this(accountName, accountType, applicationUserId) => Id = id;

        public FinancialAccount(string accountName, AccountType accountType, Guid applicationUserId) : this()
        {
            AccountName = accountName;
            AccountType = accountType;
            TenantId = applicationUserId;

            IsValid();
        }

        protected FinancialAccount() { }
    }

    public class AccountValidator : AbstractValidator<FinancialAccount>
    {
        public AccountValidator()
        {
            RuleFor(p => p.AccountName)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(50);

            RuleFor(p => p.AccountType)
                    .IsInEnum();

            RuleFor(p => p.TenantId)
                    .NotEmpty();
        }
    }
}

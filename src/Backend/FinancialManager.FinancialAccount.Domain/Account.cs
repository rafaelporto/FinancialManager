using FinancialManager.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.FinancialAccounts.Domain
{
    public class Account : Entity, IAggregateRoot
    {
        public string AccountName { get; private set; }
        public AccountType AccountType { get; private set; }
        public virtual IList<Expense> Expenses { get; set; }

        [NotMapped]
        protected override ValidationResult Validations => new AccountValidator().Validate(this);

        public Account(Guid id, string accountName, AccountType accountType, Guid applicationUserId) 
            : this(accountName, accountType, applicationUserId) => Id = id;

        public Account(string accountName, AccountType accountType, Guid applicationUserId) : this()
        {
            AccountName = accountName;
            AccountType = accountType;
            TenantId = applicationUserId;

            IsValid();
        }

        protected Account() { }

        public static Account CreateEmptyAccount(Guid id) => new() { Id = id };
    }

    public class AccountValidator : AbstractValidator<Account>
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

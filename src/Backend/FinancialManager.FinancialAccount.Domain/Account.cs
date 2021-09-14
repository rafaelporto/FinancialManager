using FinancialManager.Core;
using FinancialManager.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FinancialManager.FinancialAccounts.Domain
{
    public class Account : Entity, IAggregateRoot
    {
        public string AccountName { get; private set; }
        public AccountType AccountType { get; private set; }
        public Guid OwnerId { get; private set;  }

        [NotMapped]
        private ValidationResult Validations => new AccountValidator().Validate(this);

        public Account(Guid id, string accountName, AccountType accountType, Guid applicationUserId) 
            : this(accountName, accountType, applicationUserId) => Id = id;

        public Account(string accountName, AccountType accountType, Guid applicationUserId) : this()
        {
            AccountName = accountName;
            AccountType = accountType;
            OwnerId = applicationUserId;

            IsValid();
        }

        protected Account() { }

        public override bool IsValid()
        {
            if (Validations.IsValid)
                return true;

            _notifications = Validations.Errors?.Select(p => new Notification(p.ErrorCode, p.ErrorMessage)).ToList();
            return false;
        }

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

            RuleFor(p => p.OwnerId)
                    .NotEmpty();
        }
    }
}

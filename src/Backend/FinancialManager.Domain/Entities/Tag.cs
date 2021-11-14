using FinancialManager.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace FinancialManager.Domain
{
    public class Tag : Entity, IAggregateRoot
    {
        public string Description { get; private set; }
        public virtual ICollection<Expense> Expenses { get; private set; } = new List<Expense>();
        public virtual ICollection<FinancialAccount> FinancialAccounts { get; private set; } = new List<FinancialAccount>();

        protected override ValidationResult Validations => new CategoryValidation().Validate(this);

        public Tag(Guid id, string description, Guid tenantId) : this(description, tenantId) =>
            Id = id;

        public Tag(string description, Guid tenantId) : base()
        {
            Description = description;
            TenantId = tenantId;
            IsValid();
        }
        protected Tag() { }
    }

    public class CategoryValidation : AbstractValidator<Tag>
    {
        public CategoryValidation()
        {
            RuleFor(p => p.Description)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(1)
                    .MaximumLength(30);
        }
    }
}

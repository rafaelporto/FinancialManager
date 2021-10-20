using FinancialManager.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace FinancialManager.FinancialAccounts.Domain
{
    public class Category : Entity
    {
        public string Description { get; private set; }
        public virtual ICollection<Expense> Expenses { get; private set; }

        protected override ValidationResult Validations => new CategoryValidation().Validate(this);

        public Category(string description) : base()
        {
            Description = description;
            IsValid();
        }
        protected Category() { }
    }

    public class CategoryValidation : AbstractValidator<Category>
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

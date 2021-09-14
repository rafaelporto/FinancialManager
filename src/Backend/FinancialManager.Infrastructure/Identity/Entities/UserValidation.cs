using FinancialManager.Core.Extensions;
using FluentValidation;
using System.Linq;

namespace FinancialManager.Infrastructure.Identity
{
    public class UserValidation : AbstractValidator<ApplicationUser>
	{
		public UserValidation()
		{
			RuleFor(p => p.Email).EmailAddress();
			RuleFor(p => p.FirstName)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(20)
				.Must(StringExtensions.NotContainsSpecialCaracters).WithMessage("{PropertyName} must not have any special character.");

			RuleFor(p => p.LastName)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(50)
				.Must(StringExtensions.NotContainsSpecialCaracters).WithMessage("{PropertyName} must not have any special character.");
		}
	}
}

using System.Linq;
using FluentValidation;

namespace FinancialManager.Infra.CrossCutting.Identity
{
	internal class UserValidation : AbstractValidator<ApplicationUser>
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
				.Must(NotContainsSpecialCaracters).WithMessage("{PropertyName} must not have any special character.");

			RuleFor(p => p.LastName)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(50)
				.Must(NotContainsSpecialCaracters).WithMessage("{PropertyName} must not have any special character.");
		}

		private bool NotContainsSpecialCaracters(string value) =>
			!value.Any(s => !char.IsLetterOrDigit(s) && !char.IsWhiteSpace(s));
	}
}

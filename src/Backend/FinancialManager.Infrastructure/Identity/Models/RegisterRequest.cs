using FinancialManager.Core;
using FinancialManager.Core.Extensions;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace FinancialManager.Infrastructure.Identity
{
    public class RegisterRequest : Request
    {
        public Email? Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }

        public RegisterRequest(string email, string firstName, string lastName, string password, string confirmPassword)
        {
			Email = Core.Email.Create(email)
				.OnFailure(result => _notifications.AddRange(result.Notifications))
				.Finally<Email, Email?>(result => result.IsSuccess ? result.Value : null);

			FirstName = firstName;
			LastName = lastName;
			Password = password;
			ConfirmPassword = confirmPassword;

			if (Validations.IsValid is false)
				_notifications.AddRange(Validations.Errors.Select(s => new Notification(s.PropertyName, s.ErrorMessage)));
		}

        public override bool IsValid => base.IsValid;

        protected override ValidationResult Validations => new RegisterValidation().Validate(this);
    }

	public class RegisterValidation : AbstractValidator<RegisterRequest>
	{
		public RegisterValidation()
		{
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

			RuleFor(p => p.Password)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.NotEmpty()
				.MinimumLength(6);

			RuleFor(p => p.ConfirmPassword)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.NotEmpty()
				.Equal(p => p.Password);
		}
	}
}

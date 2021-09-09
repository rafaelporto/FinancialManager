using FinancialManager.Core;
using FinancialManager.Core.Extensions;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace FinancialManager.Infrastructure.Identity
{
    public class LoginRequest : Request
	{
        public Email? Email { get; }
        public string Password { get; }

        protected override ValidationResult Validations => new LoginCommandValidation().Validate(this);

        public LoginRequest(string email, string password)
		{
			Email = Core.Email.Create(email)
				.OnFailure(result => _notifications.AddRange(result.Notifications))
				.Finally<Email, Email?>(result => result.IsSuccess ? result.Value : null);

			Password = password;

            if (Validations.IsValid is false)
				_notifications.AddRange(Validations.Errors.Select(s => new Notification(s.PropertyName, s.ErrorMessage)));
		}

		public override bool IsValid => _notifications.Count == 0;
    }

	public class LoginCommandValidation : AbstractValidator<LoginRequest>
	{
		public LoginCommandValidation() =>
			RuleFor(p => p.Password)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.NotEmpty()
				.MinimumLength(6);
	}
}

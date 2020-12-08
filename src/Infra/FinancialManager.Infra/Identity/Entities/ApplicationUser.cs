using System;
using System.Collections.Generic;
using System.Linq;
using FinancialManager.Infra.CrossCutting.Core;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Infra.CrossCutting.Identity
{
	class ApplicationUser : IdentityUser<Guid>, IEntity
	{
		public static ApplicationUser NewUser(string firstName, string lastName, string email, string phoneNumber) =>
			new ApplicationUser
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				UserName = email,
				PhoneNumber = phoneNumber
			};

		public string FirstName { get; init; }
		public string LastName { get; init; }
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
		public IReadOnlyList<string> Notifications => Validations?.Errors?.Select(p => p.ErrorMessage).ToList();
		private ValidationResult Validations => new UserValidation().Validate(this);
		public bool IsValid => Validations.IsValid;
		public bool IsInvalid => !IsValid;
	}
}

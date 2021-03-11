using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using FinancialManager.Infra.CrossCutting.Core;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace FinancialManager.Identity
{
	public class ApplicationUser : IdentityUser, IEntity
	{
		public static ApplicationUser NewUser(string firstName, string lastName, string email, string phoneNumber) =>
			new ApplicationUser
			{
				FirstName = firstName.Trim(),
				LastName = lastName.Trim(),
				Email = email.Trim(),
				UserName = email.Trim(),
				PhoneNumber = phoneNumber.Trim()
			};

		public string FirstName { get; init; }
		public string LastName { get; init; }

		public string FullName => FirstName + " " + LastName; 
        public List<UserLoginInfo> Logins { get; set; }
		public virtual IReadOnlyList<string> Roles { get; private set; } = new List<string>();
		internal List<string> GetRolesList() => Roles as List<string>;

		[JsonIgnore]
		public IReadOnlyList<ValidationFailure> ValidationResults => Validations?.Errors.ToList();
		
		[JsonIgnore]
		private ValidationResult Validations => new UserValidation().Validate(this);

		[JsonIgnore]
		public bool IsValid => Validations.IsValid;

		[JsonIgnore]
		public bool IsInvalid => !IsValid;
	}
}

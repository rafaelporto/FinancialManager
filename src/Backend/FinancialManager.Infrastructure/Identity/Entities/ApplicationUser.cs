using FinancialManager.Core;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FinancialManager.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public static Result<ApplicationUser> Create(string firstName, string lastName, Email email)
        {
            ApplicationUser newUser = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
            };

            if (newUser.IsValid)
                return Result.Success(newUser);

            return Result.Failure<ApplicationUser>(newUser.ValidationMessages);
        }

        public ApplicationUser(string firstName, string lastName, Email email) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = email;
        }

        protected ApplicationUser() => Id = Guid.NewGuid();

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        /// <summary>
        /// IsActived define whether user is actived and not deleted
        /// </summary>
        public bool IsActived { get; protected set; } = true;

        public DateTimeOffset Created { get; protected set; }
        public DateTimeOffset? LastUpdated { get; protected set; }

        public void Delete() => IsActived = false;
        public IReadOnlyList<Notification> ValidationMessages => Validations?.Errors?.Select(p => new Notification(p.PropertyName, p.ErrorMessage)).ToList();
        
        [NotMapped]
        private ValidationResult Validations => new UserValidation().Validate(this);
        public bool IsValid => Validations.IsValid;
        public bool IsInvalid => !IsValid;
    }
}

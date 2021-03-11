using System;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace FinancialManager.Infra.ValueObjects
{
    public readonly struct Email : IComparable<Email>, IEquatable<Email>
    {
        private readonly string email;

        private Email (string email) => this.email = email.ToLower();

        static public Result<Email> Create(string email) =>
            ValidateEmail(email)
                .Finally<Result<Email>>(result => 
                result.IsSuccess ? 
                Result.Success(new Email(email)) :
                Result.Failure<Email>(result.Error));

        static public Result ValidateEmail(string email)
        {
            var patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                + @"[a-zA-Z]{2,}))$";

            Regex regexStrict = new Regex(patternStrict);

            if(regexStrict.IsMatch(email))
                return Result.Success();

            return Result.Failure("Email não válido.");
        }

        public override string ToString() => email;
        
        public int CompareTo (Email other) =>
            string.Compare(this.email, other.email, StringComparison.InvariantCultureIgnoreCase);

        public override int GetHashCode() =>
             this.email.ToLower().GetHashCode();

        public override bool Equals(object obj) =>
            obj is Email && Equals((Email)obj);

        public bool Equals(Email other) =>
             CompareTo(other) == 0;

        public static bool operator ==(Email leftHandValue, Email rightHandValue) =>
             leftHandValue.Equals(rightHandValue);

        public static bool operator !=(Email leftHandValue, Email rightHandValue ) =>
             !leftHandValue.Equals(rightHandValue);

        public static implicit operator string(Email value) => value.email;
    }
}
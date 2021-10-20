using FinancialManager.Core.Extensions;
using System;

namespace FinancialManager.Core
{
    public readonly struct Amount
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            Value = default;
            var result = Validate(value);

            if (result.IsSuccess)
                Value = Math.Round(value, 2);
        }

        static public Result<Amount> Create(decimal amount) =>
            Validate(amount)
                .Finally(result => result.IsSuccess ?
                    Result.Success(new Amount(amount)) :
                    Result.Failure<Amount>(result.Notifications));

        static public Result Validate(decimal value)
        {
            if (value < 0)
                return Result.Failure(new Notification(nameof(Amount), "Amount can not be less than 0."));

            Math.Round(value, 2);

            return Result.Success();
        }

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is Amount amount && Equals(amount);

        public bool Equals(Amount other) => this.Value == other.Value;

        public static bool operator ==(Amount leftHandValue, Amount rightHandValue) =>
             leftHandValue.Equals(rightHandValue);

        public static bool operator !=(Amount leftHandValue, Amount rightHandValue) =>
             !leftHandValue.Equals(rightHandValue);

        public static implicit operator decimal(Amount amount) => amount.Value;
        public static implicit operator Amount(decimal amount) => new(amount);
    }
}

using System.Collections.Generic;

namespace FinancialManager.Core
{
    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(bool isSuccess, T value, IEnumerable<Notification> notifications = default)
            : base(isSuccess, notifications) => Value = value;

        public static Result<T> Success(T value) => new(true, value);
        public static new Result<T> Failure(IEnumerable<Notification> notifications) => new(false, default, notifications);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Notification> Notifications { get; }
        protected Result(bool isSuccess, IEnumerable<Notification> notifications = default)
        {
            IsSuccess = isSuccess;
            Notifications = notifications as IReadOnlyList<Notification>;
        }
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result<T> Failure<T>(IEnumerable<Notification> notifications) => Result<T>.Failure(notifications);
        public static Result<T> Failure<T>(Notification notification) => Result<T>.Failure(new Notification[] { notification });
        public static Result Success() => new(true);
        public static Result Failure(IEnumerable<Notification> notifications) => new(false, notifications);
        public static Result Failure(Notification notification) => new(false, new Notification[] { notification });
    }
}

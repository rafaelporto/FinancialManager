using System;
using System.Threading.Tasks;

namespace FinancialManager.Core.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        ///     Creates a new result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static Result<K> Map<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Failure<K>(result.Notifications);

            return func(result.Value);
        }

        /// <summary>
        ///     Creates a new result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static async Task<Result<K>> Map<T, K>(this Result<T> result, Func<T, Task<Result<K>>> func)
        {
            if (result.IsFailure)
                return Result.Failure<K>(result.Notifications);

            Result<K> value = await func(result.Value).ConfigureAwait(false);
            return value;
        }

        public static T Finally<T>(this Result result, Func<Result, T> func)
            => func(result);

        public static K Finally<T, K>(this Result<T> result, Func<Result<T>, K> func)
            => func(result);

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<Result<T>> action)
        {
            if (result.IsFailure)
                action(result);

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success. Returns the calling result.
        /// </summary>
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<Result<T>> action)
        {
            if (result.IsSuccess)
                action(result);

            return result;
        }
    }
}

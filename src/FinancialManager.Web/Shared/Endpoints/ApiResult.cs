using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinancialManager.Endpoints
{
    public class ApiResult<T>
    {
        public T Content { get; set; }
        public virtual IReadOnlyList<string> Notifications { get; }
        public virtual bool IsSuccessed => Notifications is null || Notifications.Count == 0;
        public virtual bool IsFailure => IsSuccessed is false;

        [JsonConstructor]
        public ApiResult(T content, IReadOnlyList<string> notifications = default) =>
            (Content, Notifications) = (content, notifications);

        public static ApiResult<T> Success(T content) =>new ApiResult<T>(content);
        public static ApiResult<T> Failure(string[] notifications) => new ApiResult<T>(default, notifications);
        public static ApiResult<T> Failure(string error) => new ApiResult<T>(default, new string[] { error });
    }

    public class ApiResult
    {
        public static ApiResult<T> Success<T>(T content) => new ApiResult<T>(content);
        public static ApiResult<T> Failure<T>(string[] notifications) => new ApiResult<T>(default, notifications);
        public static ApiResult<T> Failure<T>(string error) => new ApiResult<T>(default, new string[] { error });
    }
}

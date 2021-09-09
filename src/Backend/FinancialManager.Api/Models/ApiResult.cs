using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinancialManager.Api
{
    public class ApiResult<T> : ApiResult
    {
        public T Content { get; set; }
        
        [JsonConstructor]
        public ApiResult(T content, IDictionary<string, string> notifications = default) : base(notifications) =>
            Content = content;

        public static ApiResult<T> Success(T content) => new(content);
    }

    public class ApiResult
    {
        public virtual IReadOnlyDictionary<string, string> Notifications { get; }

        [JsonIgnore]
        public virtual bool IsSuccessed => Notifications is null || Notifications.Count == 0;

        [JsonIgnore]
        public virtual bool IsFailure => IsSuccessed is false;

        protected ApiResult(IDictionary<string, string> notifications = default) =>
            Notifications = notifications as IReadOnlyDictionary<string, string>;

        public static ApiResult Success() => new();
        public static ApiResult Failure(IDictionary<string, string> notifications) => new(notifications);
        public static ApiResult<T> Success<T>(T content) => new(content);
        public static ApiResult<T> Failure<T>(IDictionary<string, string> notifications) => new(default, notifications);
    }
}

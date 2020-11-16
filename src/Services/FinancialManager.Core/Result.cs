using System.Collections.Generic;
using System.Linq;

namespace FinancialManager.Core
{
	public record Result<T>
	{
		public bool IsSuccess => _errors.Any() == false;
		public bool IsFailure => !IsSuccess;
		private List<string> _errors;
		public IReadOnlyList<string> Errors => _errors;
		public T Value { get; }

		private Result(T value) => Value = value;
		
		private Result(IEnumerable<string> errors) =>
			_errors = errors?.ToList() ?? new List<string>();

		public static Result<T> Ok(T value) => new Result<T>(value);
		public static Result<T> Fail(IEnumerable<string> errors) => new Result<T>(errors);
	}
}

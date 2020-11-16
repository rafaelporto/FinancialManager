using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace FinancialManager.Core.Communication.Mediator
{
	public abstract class Command<T> : IRequest<Result<T>>
	{
		public DateTimeOffset Timestamp { get; init; }
		public ValidationResult ValidationResult { get; set; }

		protected Command() => Timestamp = DateTimeOffset.UtcNow;

		public virtual bool IsValid() => throw new NotImplementedException("Validação não implementada");
	}
}

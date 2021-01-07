using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace FinancialManager.Infra.CrossCutting.Core
{
	public interface IEntity
	{
		IReadOnlyList<ValidationFailure> ValidationResults { get; }

		ValidationResult Validations { get; }

		bool IsValid { get; }

		bool IsInvalid { get; }
	}
}

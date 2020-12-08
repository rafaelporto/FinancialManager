using System;

namespace FinancialManager.Infra.CrossCutting.Core
{
	public interface IEntity
	{
		DateTimeOffset CreatedDate { get; set; }
		DateTimeOffset UpdatedDate { get; set; }
	}
}

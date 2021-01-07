namespace FinancialManager.Infra.CrossCutting.Core
{
	public interface IEntity
	{
		bool IsValid { get; }

		bool IsInvalid { get; }
	}
}

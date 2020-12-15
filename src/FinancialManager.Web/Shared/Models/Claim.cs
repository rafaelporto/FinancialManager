namespace FinancialManager.Web.Shared.Models
{
	public record Claim
	{
		public string Value { get; init; }
		public string Type { get; init; }

		public Claim(string type, string value) =>
			(Type, Value) = (type, value);
	}
}

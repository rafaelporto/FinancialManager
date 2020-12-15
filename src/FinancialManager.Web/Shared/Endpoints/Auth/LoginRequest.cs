namespace FinancialManager.Web.Shared.Endpoints
{
	public record LoginRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}

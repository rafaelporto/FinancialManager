namespace FinancialManager.Web.Shared.Endpoints
{
	public record RegisterUserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string PhoneNumber { get; set; }
	}
}

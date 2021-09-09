using System.ComponentModel.DataAnnotations;

namespace FinancialManager.Api
{
    public record LoginModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}

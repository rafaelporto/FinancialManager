using FinancialManager.Core.Communication.Mediator;

namespace FinancialManager.Identity.Application.Commands
{
	public class LoginCommand : Command<bool>
	{
		public string Email { get; init; }
		public string Password { get; init; }
	}
}

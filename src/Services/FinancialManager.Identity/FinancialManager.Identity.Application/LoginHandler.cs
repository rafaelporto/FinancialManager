using System.Threading;
using System.Threading.Tasks;
using FinancialManager.Core;
using FinancialManager.Core.Communication.Mediator;
using FinancialManager.Identity.Application.Commands;

namespace FinancialManager.Identity.Application
{
	public class LoginHandler : RequestHandler<LoginCommand,Result<bool>>
	{
		public LoginHandler()
		{

		}

		public override Task<Result<bool>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			return base.Handle(request, cancellationToken);
		}
	}
}

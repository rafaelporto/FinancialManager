using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FinancialManager.Core.Communication.Mediator
{
	public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
		public virtual Task<TResult> Handle(TRequest request, CancellationToken cancellationToken) =>
			throw new NotImplementedException("Command not implemented yet.");
	}
}

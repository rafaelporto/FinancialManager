using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator) => _mediator = mediator;

        public async Task PublishEvent<T>(T @event, CancellationToken token = default)
            where T : Event => await _mediator.Publish(@event, token);

        public async Task PublishNotification<T>(T notification, CancellationToken token = default)
            where T : Notification => await _mediator.Publish(notification, token);

        public async Task<bool> SendCommand<T>(T command, CancellationToken token = default)
            where T : Command => await _mediator.Send(command, token);
    }
}

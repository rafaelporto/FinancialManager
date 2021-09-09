using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Core.Communication
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event, CancellationToken token = default) where T : Event;
        Task PublishNotification<T>(T notification, CancellationToken token = default) where T : Notification;
        Task<bool> SendCommand<T>(T command, CancellationToken token = default) where T : Command;
    }
}

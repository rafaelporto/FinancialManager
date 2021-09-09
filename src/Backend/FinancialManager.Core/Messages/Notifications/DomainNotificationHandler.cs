using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialManager.Core
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        private List<Notification> _notifications;

        public NotificationHandler() => _notifications = new List<Notification>();

        public Task Handle(Notification message, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
                _notifications.Add(message);
                
            return Task.CompletedTask;
        }

        public virtual List<Notification> GetNotifications() => _notifications;

        public virtual bool HasNotification() => _notifications.Count > 0;

        public void Dispose() => _notifications = new List<Notification>();
    }
}

using System;
using System.Collections.Generic;

namespace FinancialManager.Core
{
    public interface IScopeControl
    {
        IReadOnlyList<Notification> Notifications { get; }
        bool HasNotification { get; }

        void AddNotification(Notification notification);

        bool TryAddNotification(string key, string description);

        void AddNotifications(IEnumerable<Notification> notifications);

        Guid GetUserId();

        string GetUserName();

        string GetUserEmail();
    }
}
 
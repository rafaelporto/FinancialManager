using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialManager.Core
{
    public class ScopeControl : IScopeControl
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly List<Notification> _notifications;

        public ScopeControl(IAspNetUser aspNetUser) => 
            (_aspNetUser, _notifications) = (aspNetUser, new());

        public IReadOnlyList<Notification> Notifications => _notifications;

        public bool HasNotification => _notifications.Any();

        public void AddNotification(Notification notification) =>
            _notifications.Add(notification);

        public void AddNotifications(IEnumerable<Notification> notifications) =>
            _notifications.AddRange(notifications);

        public bool TryAddNotification(string key, string description)
        {
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(key))
                return false;

            _notifications.Add(new Notification(key, description));
            return true;
        }

        public string GetUserEmail() => _aspNetUser.Mail;

        public Guid GetUserId() => _aspNetUser.Id;

        public string GetUserName() => _aspNetUser.Name;
    }
}

using System.Collections.Generic;

namespace FinancialManager.Core.Extensions
{
    public static class DomainNotificationExtensions
    {
        public static IReadOnlyList<Notification> MapToDomainNotification(this IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            List<Notification> result = new();

            foreach (var item in keyValues)
                result.Add(new Notification(item.Key, item.Value));

            return result.AsReadOnly();
        }

        public static Dictionary<string,string> MapToDictionary(this IEnumerable<Notification> notifications)
        {
            Dictionary<string, string> result = new();

            foreach (var item in notifications)
                result.Add(item.Key, item.Value);

            return result;
        }
    }
}

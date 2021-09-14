using System;

namespace FinancialManager.Core
{
    public class Notification
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Notification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}

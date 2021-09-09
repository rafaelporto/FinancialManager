using MediatR;
using System;

namespace FinancialManager.Core
{
    public abstract class Event : IMessage, INotification
    {
        public DateTimeOffset Timestamp { get; private set; }

        protected Event() => Timestamp = DateTimeOffset.Now;
    }
}

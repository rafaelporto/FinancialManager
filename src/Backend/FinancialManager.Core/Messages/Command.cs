using MediatR;
using System;

namespace FinancialManager.Core
{
    public abstract class Command : Request, IMessage, IRequest<bool>
    {
        public DateTimeOffset Timestamp { get; private set; }

        protected Command() => (Timestamp, _notifications) = (DateTimeOffset.Now, new());
    }
}

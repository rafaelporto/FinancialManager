using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace FinancialManager.Core
{
    public abstract class Request
    {
        protected List<Notification> _notifications;
        public IReadOnlyList<Notification> Notifications => _notifications;
        protected virtual ValidationResult Validations => throw new NotImplementedException($"{nameof(Validations)} is not implemented.");

        protected Request() => _notifications = new();

        public virtual bool IsValid => Validations.IsValid;
        public virtual bool IsInValid => !IsValid;
    }
}

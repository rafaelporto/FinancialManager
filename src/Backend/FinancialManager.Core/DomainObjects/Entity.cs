using System;
using System.Collections.Generic;

namespace FinancialManager.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public List<Notification> _notifications;
        public IReadOnlyList<Notification> Notifications => _notifications?.AsReadOnly();
        public DateTimeOffset Created { get; protected set; }
        public DateTimeOffset? LastUpdated { get; protected set; }
        public bool IsDeleted { get; private set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) =>  !(a == b);
        

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id={Id}]";

        public virtual bool IsValid() => throw new NotImplementedException("IsValid is not implemented.");
        public virtual bool IsInValid() => !IsValid();
        public void Delete() => IsDeleted = false;
    }
}

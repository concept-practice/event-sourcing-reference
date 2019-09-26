using System;
using MediatR;

namespace EventSourcingReference.EventSourcing
{
    public abstract class DomainEvent : INotification
    {
        public Guid EntityId { get; }
        public DateTime EventDateTime { get; }

        protected DomainEvent(Guid entityId)
        {
            EntityId = entityId;
            EventDateTime = DateTime.UtcNow;
        }
    }
}

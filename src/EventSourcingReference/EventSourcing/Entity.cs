using System;

namespace EventSourcingReference.EventSourcing
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}

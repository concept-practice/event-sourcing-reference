using System;
using EventSourcingReference.EventSourcing;

namespace EventSourcingReference.Write
{
    public class AttractionCreated : DomainEvent
    {
        public string Name { get; }
        public int CurrentRiders { get; }

        public AttractionCreated(Guid entityId, string name, int currentRiders) : base(entityId)
        {
            Name = name;
            CurrentRiders = currentRiders;
        }
    }
}

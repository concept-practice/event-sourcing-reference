using System;
using EventSourcingReference.EventSourcing;

namespace EventSourcingReference.Write
{
    public class AttractionClosed : DomainEvent
    {
        public int CurrentRiders { get; }

        public AttractionClosed(Guid entityId, int currentRiders) : base(entityId)
        {
            CurrentRiders = currentRiders;
        }
    }
}

using System;
using EventSourcingReference.EventSourcing;

namespace EventSourcingReference.Write
{
    public class AttractionCycleCompleted : DomainEvent
    {
        public int CurrentRiders { get; }

        public AttractionCycleCompleted(Guid entityId, int currentRiders) : base(entityId)
        {
            CurrentRiders = currentRiders;
        }
    }
}

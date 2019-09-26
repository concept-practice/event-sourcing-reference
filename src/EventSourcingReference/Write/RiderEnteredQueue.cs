using System;
using EventSourcingReference.EventSourcing;

namespace EventSourcingReference.Write
{
    public class RiderEnteredQueue : DomainEvent
    {
        public int CurrentRiders { get; }

        public RiderEnteredQueue(Guid entityId, int currentRiders) : base(entityId)
        {
            CurrentRiders = currentRiders;
        }
    }
}

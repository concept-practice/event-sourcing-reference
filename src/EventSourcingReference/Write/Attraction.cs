using System;
using EventSourcingReference.EventSourcing;

namespace EventSourcingReference.Write
{
    public class Attraction : AggregateRoot
    {
        public string Name { get; private set; }
        public int CurrentRiders { get; private set; }

        private Attraction() { }

        public Attraction(string name)
        {
            ApplyEvent(new AttractionCreated(Guid.NewGuid(), name, 0));
        }

        private void AttractionCreated(AttractionCreated e)
        {
            Id = e.EntityId;
            Name = e.Name;
            CurrentRiders = e.CurrentRiders;
        }

        public void RiderEnteredQueue()
        {
            ApplyEvent(new RiderEnteredQueue(Id, ++CurrentRiders));
        }

        private void RiderEnteredQueue(RiderEnteredQueue e)
        {
            CurrentRiders = e.CurrentRiders;
        }

        public void AttractionCycleCompleted()
        {
            const int ridersPerCycle = 6;

            ApplyEvent(CurrentRiders <= ridersPerCycle
                ? new AttractionCycleCompleted(Id, 0)
                : new AttractionCycleCompleted(Id, CurrentRiders - ridersPerCycle));
        }

        private void AttractionCycleCompleted(AttractionCycleCompleted e)
        {
            CurrentRiders = e.CurrentRiders;
        }

        public void Close()
        {
            ApplyEvent(new AttractionClosed(Id, 0));
        }

        private void AttractionClosed(AttractionClosed e)
        {
            CurrentRiders = e.CurrentRiders;
        }
    }
}

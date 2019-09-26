using System;

namespace EventSourcingReference.EventSourcing
{
    public class EventStream
    {
        public Guid Id { get; private set; }
        public Guid EntityId { get; private set; }
        public DateTime EventDateTime { get; private set; }
        public string EventType { get; private set; }
        public string Data { get; private set; }
        public int Version { get; private set; }

        private EventStream() { }

        public EventStream(DomainEvent domainEvent, string data, int version)
        {
            Id = Guid.NewGuid();
            EntityId = domainEvent.EntityId;
            EventDateTime = domainEvent.EventDateTime;
            EventType = domainEvent.GetType().ToString();
            Data = data;
            Version = version;
        }
    }
}

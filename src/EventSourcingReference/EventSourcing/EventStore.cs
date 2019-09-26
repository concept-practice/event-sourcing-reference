using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EventSourcingReference.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly AppContext _appContext;
        private readonly IMediator _mediator;

        public EventStore(AppContext appContext, IMediator mediator)
        {
            _appContext = appContext;
            _mediator = mediator;
        }

        public async Task Save(AggregateRoot aggregate)
        {
            var currentVersion = aggregate.CurrentEventVersion;

            var eventStreams = aggregate.Events.Select(domainEvent => new EventStream(domainEvent, SerializeEvent(domainEvent), currentVersion++));

            await _appContext.EventStreams.AddRangeAsync(eventStreams);
            await _appContext.SaveChangesAsync();

            foreach (var domainEvent in aggregate.Events)
            {
                await _mediator.Publish(domainEvent);
            }
        }

        public async Task<IEnumerable<DomainEvent>> GetEvents(Guid id)
        {
            var streams = await _appContext.EventStreams
                .Where(x => x.EntityId == id)
                .OrderBy(x => x.Version)
                .ToListAsync();

            return streams.Select(DeserializeEvent);
        }

        public async Task<IEnumerable<DomainEvent>> GetEvents(Guid id, DateTime dateTime)
        {
            var streams = await _appContext.EventStreams
                .Where(x => x.EntityId == id)
                .Where(x => x.EventDateTime <= dateTime)
                .OrderBy(x => x.Version)
                .ToListAsync();

            return streams.Select(DeserializeEvent);
        }

        public async Task<IEnumerable<DomainEvent>> GetEvents(Guid id, int version)
        {
            var streams = await _appContext.EventStreams
                .Where(x => x.EntityId == id)
                .Where(x => x.Version <= version)
                .OrderBy(x => x.Version)
                .ToListAsync();

            return streams.Select(DeserializeEvent);
        }

        public async Task UpdateEvent<TEvent>()
        {
            var newEventStreams = (await _appContext.EventStreams
                    .Where(eventStream => eventStream.EventType == typeof(TEvent).ToString())
                    .ToListAsync())
                    .Select(domainEvent =>
                    {
                        _appContext.EventStreams.Remove(domainEvent);
                        var newDomainEvent = (TEvent)Activator.CreateInstance(typeof(TEvent), domainEvent) as DomainEvent;
                        return new EventStream(newDomainEvent, SerializeEvent(newDomainEvent), domainEvent.Version);
                    });

            await _appContext.EventStreams.AddRangeAsync(newEventStreams);
            await _appContext.SaveChangesAsync();
        }

        private DomainEvent DeserializeEvent(EventStream stream)
        {
            return JsonConvert.DeserializeObject(stream.Data, Type.GetType(stream.EventType)) as DomainEvent;
        }

        private string SerializeEvent(DomainEvent @event)
        {
            return JsonConvert.SerializeObject(@event);
        }
    }
}

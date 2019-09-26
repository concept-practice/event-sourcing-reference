using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingReference.EventSourcing
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        private readonly IEventStore _eventStore;

        public Repository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<TAggregateRoot> Get(Guid id)
        {
            var events = await _eventStore.GetEvents(id);
            return ConstructObject(events);
        }

        public async Task<TAggregateRoot> Get(Guid id, DateTime dateTime)
        {
            var events = await _eventStore.GetEvents(id, dateTime);
            return ConstructObject(events);
        }

        public async Task Save(AggregateRoot aggregate)
        {
            await _eventStore.Save(aggregate);
        }

        private TAggregateRoot ConstructObject(IEnumerable<DomainEvent> events)
        {
            var obj = (TAggregateRoot)Activator.CreateInstance(typeof(TAggregateRoot), true);
            obj.LoadHistory(events);
            return obj;
        }
    }
}

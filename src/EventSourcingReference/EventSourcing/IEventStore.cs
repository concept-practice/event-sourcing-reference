using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingReference.EventSourcing
{
    public interface IEventStore
    {
        Task Save(AggregateRoot aggregate);
        Task<IEnumerable<DomainEvent>> GetEvents(Guid id);
        Task<IEnumerable<DomainEvent>> GetEvents(Guid id, DateTime dateTime);
        Task<IEnumerable<DomainEvent>> GetEvents(Guid id, int version);
        Task UpdateEvent<TEvent>();
    }
}

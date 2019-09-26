using System;
using System.Threading.Tasks;

namespace EventSourcingReference.EventSourcing
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        Task Save(AggregateRoot aggregate);
        Task<TAggregateRoot> Get(Guid id);
        Task<TAggregateRoot> Get(Guid id, DateTime dateTime);
    }
}

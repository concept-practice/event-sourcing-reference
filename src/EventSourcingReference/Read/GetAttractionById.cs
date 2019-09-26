using System;
using MediatR;

namespace EventSourcingReference.Read
{
    public class GetAttractionById : IRequest<Attraction>
    {
        public Guid AttractionId { get; set; }
    }
}

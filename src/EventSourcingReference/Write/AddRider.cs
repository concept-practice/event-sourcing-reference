using System;
using MediatR;

namespace EventSourcingReference.Write
{
    public class AddRider : IRequest
    {
        public Guid AttractionId { get; set; }
    }
}

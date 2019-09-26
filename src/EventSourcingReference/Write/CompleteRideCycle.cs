using System;
using MediatR;

namespace EventSourcingReference.Write
{
    public class CompleteRideCycle : IRequest
    {
        public Guid AttractionId { get; set; }
    }
}

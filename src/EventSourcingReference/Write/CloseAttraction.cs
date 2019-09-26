using System;
using MediatR;

namespace EventSourcingReference.Write
{
    public class CloseAttraction : IRequest
    {
        public Guid AttractionId { get; set; }
    }
}

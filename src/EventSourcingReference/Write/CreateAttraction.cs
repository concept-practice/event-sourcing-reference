using MediatR;

namespace EventSourcingReference.Write
{
    public class CreateAttraction : IRequest
    {
        public string Name { get; set; }
    }
}

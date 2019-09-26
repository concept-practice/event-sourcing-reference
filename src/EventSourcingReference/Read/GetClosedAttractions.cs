using MediatR;

namespace EventSourcingReference.Read
{
    public class GetClosedAttractions : IRequest<ClosedAttractions>
    {
    }
}

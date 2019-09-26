using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingReference.Read
{
    public class AttractionQueryHandler :
        IRequestHandler<GetAttractionById, Attraction>,
        IRequestHandler<GetAllAttractions, AllAttractions>,
        IRequestHandler<GetClosedAttractions, ClosedAttractions>
    {
        private readonly AttractionContext _context;

        public AttractionQueryHandler(AttractionContext context)
        {
            _context = context;
        }

        public async Task<Attraction> Handle(GetAttractionById request, CancellationToken cancellationToken)
        {
            return await _context.Attractions.FindAsync(request.AttractionId);
        }

        public async Task<AllAttractions> Handle(GetAllAttractions request, CancellationToken cancellationToken)
        {
            var attractions = await _context.Attractions.ToListAsync(cancellationToken);

            return new AllAttractions(attractions);
        }

        public async Task<ClosedAttractions> Handle(GetClosedAttractions request, CancellationToken cancellationToken)
        {
            var attractions = await _context.Attractions
                .Where(x => x.Status == AttractionStatus.Closed)
                .ToListAsync(cancellationToken);

            return new ClosedAttractions(attractions);
        }
    }
}

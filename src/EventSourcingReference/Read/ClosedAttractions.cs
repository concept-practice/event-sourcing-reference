using System.Collections.Generic;

namespace EventSourcingReference.Read
{
    public class ClosedAttractions
    {
        public IEnumerable<Attraction> Attractions { get; }

        public ClosedAttractions(IEnumerable<Attraction> attractions)
        {
            Attractions = attractions;
        }
    }
}

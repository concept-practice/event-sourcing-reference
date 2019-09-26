using System.Collections.Generic;

namespace EventSourcingReference.Read
{
    public class AllAttractions
    {
        public IEnumerable<Attraction> Attractions { get; }

        public AllAttractions(IEnumerable<Attraction> attractions)
        {
            Attractions = attractions;
        }
    }
}

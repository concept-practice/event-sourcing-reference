using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingReference.Write;
using MediatR;

namespace EventSourcingReference.Read
{
    public class AttractionNotificationHandler :
        INotificationHandler<AttractionCreated>,
        INotificationHandler<RiderEnteredQueue>,
        INotificationHandler<AttractionCycleCompleted>,
        INotificationHandler<AttractionClosed>
    {
        private readonly AttractionContext _attractionContext;

        public AttractionNotificationHandler(AttractionContext attractionContext)
        {
            _attractionContext = attractionContext;
        }

        public async Task Handle(AttractionCreated notification, CancellationToken cancellationToken)
        {
            var attraction = new Attraction
            {
                Id = notification.EntityId,
                Status = AttractionStatus.Open,
                CurrentRiders = notification.CurrentRiders,
                Name = notification.Name,
                CurrentWaitTime = new TimeSpan(0, 0, 0)
            };

            await _attractionContext.Attractions.AddAsync(attraction, cancellationToken);
            await _attractionContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(RiderEnteredQueue notification, CancellationToken cancellationToken)
        {
            var attraction = await _attractionContext.Attractions.FindAsync(notification.EntityId);

            attraction.CurrentRiders = notification.CurrentRiders;
            attraction.CurrentWaitTime += new TimeSpan(0, 1, 0); 

            await _attractionContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(AttractionCycleCompleted notification, CancellationToken cancellationToken)
        {
            var attraction = await _attractionContext.Attractions.FindAsync(notification.EntityId);

            attraction.CurrentRiders = notification.CurrentRiders;
            attraction.CurrentWaitTime -= new TimeSpan(0, 5, 0);

            await _attractionContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(AttractionClosed notification, CancellationToken cancellationToken)
        {
            var attraction = await _attractionContext.Attractions.FindAsync(notification.EntityId);

            attraction.CurrentRiders = notification.CurrentRiders;
            attraction.CurrentWaitTime = new TimeSpan(0, 0, 0);
            attraction.Status = AttractionStatus.Closed;

            await _attractionContext.SaveChangesAsync(cancellationToken);
        }
    }
}

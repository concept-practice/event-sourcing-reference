using System.Threading;
using System.Threading.Tasks;
using EventSourcingReference.EventSourcing;
using MediatR;

namespace EventSourcingReference.Write
{
    public class AttractionCommandHandler :
        IRequestHandler<CreateAttraction>,
        IRequestHandler<AddRider>,
        IRequestHandler<CompleteRideCycle>,
        IRequestHandler<CloseAttraction>
    {
        private readonly IRepository<Attraction> _repository;

        public AttractionCommandHandler(IRepository<Attraction> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateAttraction request, CancellationToken cancellationToken)
        {
            var attraction = new Attraction(request.Name);

            await _repository.Save(attraction);

            return await Unit.Task;
        }


        public async Task<Unit> Handle(AddRider request, CancellationToken cancellationToken)
        {
            var attraction = await _repository.Get(request.AttractionId);

            attraction.RiderEnteredQueue();

            await _repository.Save(attraction);

            return await Unit.Task;
        }

        public async Task<Unit> Handle(CompleteRideCycle request, CancellationToken cancellationToken)
        {
            var attraction = await _repository.Get(request.AttractionId);

            attraction.AttractionCycleCompleted();

            await _repository.Save(attraction);

            return await Unit.Task;
        }

        public async Task<Unit> Handle(CloseAttraction request, CancellationToken cancellationToken)
        {
            var attraction = await _repository.Get(request.AttractionId);

            attraction.Close();

            await _repository.Save(attraction);

            return await Unit.Task;
        }
    }
}

using System;
using System.Threading.Tasks;
using Autofac;
using EventSourcingReference.Read;
using EventSourcingReference.Write;
using MediatR;

namespace EventSourcingReference
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var container = Bootstrapper.Container();

            using (var scope = container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                //await mediator.Send(new CreateAttraction { Name = "Runaway Mine Train" });
                //await mediator.Send(new CreateAttraction { Name = "Log Jam" });

                //for (var i = 0; i < 10; i++)
                //{
                //    await mediator.Send(new AddRider { AttractionId = Guid.Parse("7FD183C9-2CA9-4441-BCAE-8E49473E6E9B") });
                //    await mediator.Send(new AddRider { AttractionId = Guid.Parse("476A2AB7-E68E-4CB2-9802-757DD5E90F02") });
                //}

                //await mediator.Send(new CompleteRideCycle {AttractionId = Guid.Parse("476A2AB7-E68E-4CB2-9802-757DD5E90F02") });

                //await mediator.Send(new CloseAttraction {AttractionId = Guid.Parse("7FD183C9-2CA9-4441-BCAE-8E49473E6E9B")});

                var attractions = await mediator.Send(new GetClosedAttractions());

                foreach(var i in attractions.Attractions) Console.WriteLine(i);
            }

            Console.WriteLine("...Finished");
            Console.ReadLine();
        }
    }
}

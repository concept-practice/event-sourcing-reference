using System.Reflection;
using Autofac;
using EventSourcingReference.EventSourcing;
using EventSourcingReference.Read;
using MediatR;

namespace EventSourcingReference
{
    public static class Bootstrapper
    {
        public static IContainer Container()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppContext>()
                .AsSelf();

            builder.RegisterType<AttractionContext>()
                .AsSelf();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));

            builder.RegisterType<EventStore>()
                .As<IEventStore>();

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.Name.EndsWith("Handler"))
                .AsImplementedInterfaces();

            return builder.Build();
        }
    }
}

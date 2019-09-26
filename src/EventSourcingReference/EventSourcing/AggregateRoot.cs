using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EventSourcingReference.EventSourcing
{
    public abstract class AggregateRoot : Entity
    {
        private readonly IList<DomainEvent> _notifications;
        public int CurrentEventVersion { get; private set; } = 0;
        public IEnumerable<DomainEvent> Events => _notifications.AsEnumerable();

        protected AggregateRoot()
        {
            _notifications = new List<DomainEvent>();
        }

        public void LoadHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var notification in history)
            {
                ApplyEvent(notification, false);
                CurrentEventVersion++;
            }
        }

        protected void ApplyEvent(DomainEvent notification)
        {
            ApplyEvent(notification, true);
        }

        private void ApplyEvent(DomainEvent notification, bool isNew)
        {
            Apply(notification);
            if (isNew) _notifications.Add(notification);
        }

        private void Apply(DomainEvent notification)
        {
            var function = (from method in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                from parameter in method.GetParameters()
                where parameter.ParameterType == notification.GetType()
                select method).First();

            function.Invoke(this, new object[] { notification });
        }
    }
}

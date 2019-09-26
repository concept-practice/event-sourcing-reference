using System;

namespace EventSourcingReference.Read
{
    public class Attraction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentRiders { get; set; }
        public AttractionStatus Status { get; set; }
        public TimeSpan CurrentWaitTime { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                   $"CurrentRiders: {CurrentRiders}\n" +
                   $"Status: {Status.ToString()}\n" +
                   $"WaitTime: {CurrentWaitTime}\n";
        }
    }
}

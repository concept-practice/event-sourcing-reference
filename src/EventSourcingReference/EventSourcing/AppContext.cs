using Microsoft.EntityFrameworkCore;

namespace EventSourcingReference.EventSourcing
{
    public sealed class AppContext : DbContext
    {
        public DbSet<EventStream> EventStreams { get; set; }

        public AppContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=EventSourcing;Trusted_Connection=True;");
        }
    }
}

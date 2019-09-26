using Microsoft.EntityFrameworkCore;

namespace EventSourcingReference.Read
{
    public sealed class AttractionContext : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; }

        public AttractionContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=Attractions;Trusted_Connection=True;");
        }
    }
}

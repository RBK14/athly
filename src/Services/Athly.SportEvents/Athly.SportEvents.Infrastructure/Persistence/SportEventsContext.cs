using Athly.SportEvents.Domain.SportEventAggregate;
using Microsoft.EntityFrameworkCore;

namespace Athly.SportEvents.Infrastructure.Persistence;

public class SportEventsContext : DbContext
{
    public SportEventsContext(DbContextOptions<SportEventsContext> options) : base(options) { }

    public DbSet<SportEvent> SportEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportEventsContext).Assembly);
    }
}
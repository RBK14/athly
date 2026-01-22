using Athly.SportEvents.Application.Interfaces;
using Athly.SportEvents.Domain.Cities;
using Athly.SportEvents.Domain.SportEvents;
using Athly.SportEvents.Domain.Venues;
using Microsoft.EntityFrameworkCore;

namespace Athly.SportEvents.Infrastructure.Persistence;

public class SportEventsContext : DbContext, ISportEventsContext
{
    public SportEventsContext(DbContextOptions<SportEventsContext> options) : base(options) { }

    public DbSet<SportEvent> SportEvents { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportEventsContext).Assembly);
    }
}
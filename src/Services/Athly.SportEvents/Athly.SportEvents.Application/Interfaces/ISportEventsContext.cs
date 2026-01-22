using Athly.SportEvents.Domain.Cities;
using Athly.SportEvents.Domain.SportEvents;
using Athly.SportEvents.Domain.Venues;
using Microsoft.EntityFrameworkCore;

namespace Athly.SportEvents.Application.Interfaces
{
    public interface ISportEventsContext
    {
        DbSet<SportEvent> SportEvents { get; }
        DbSet<Venue> Venues { get; }
        DbSet<City> Cities { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

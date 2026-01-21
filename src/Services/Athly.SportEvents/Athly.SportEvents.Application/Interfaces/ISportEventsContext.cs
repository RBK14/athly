using Athly.SportEvents.Domain.SportEventAggregate;
using Microsoft.EntityFrameworkCore;

namespace Athly.SportEvents.Application.Interfaces
{
    public interface ISportEventsContext
    {
        DbSet<SportEvent> SportEvents { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

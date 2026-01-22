using Athly.SportEvents.Application.SportEvents.Common;
using MediatR;

namespace Athly.SportEvents.Application.SportEvents.Queries.GetNearbySportEvents
{
    public record GetNearbySportEventsQuery(
        double UserLatitude,
        double UserLongitude,
        double RadiusInKm,
        string? Sport = null,
        DateTimeOffset? FromDate = null,
        DateTimeOffset? ToDate = null) : IRequest<IEnumerable<SportEventDto>>;
}

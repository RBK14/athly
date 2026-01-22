using Athly.SportEvents.Application.Interfaces;
using Athly.SportEvents.Application.SportEvents.Common;
using Athly.SportEvents.Domain.SportEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Athly.SportEvents.Application.SportEvents.Queries.GetNearbySportEvents;

public class GetNearbySportEventsHandler(ISportEventsContext context)
    : IRequestHandler<GetNearbySportEventsQuery, IEnumerable<SportEventDto>>
{
    private readonly ISportEventsContext _context = context;
    private readonly GeometryFactory _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    public async Task<IEnumerable<SportEventDto>> Handle(GetNearbySportEventsQuery request, CancellationToken cancellationToken)
    {
        var userLocation = _geometryFactory.CreatePoint(new Coordinate(request.UserLongitude, request.UserLatitude));
        double radiusInMeters = request.RadiusInKm * 1000;

        var eventsQuery = _context.SportEvents.AsNoTracking();

        eventsQuery = eventsQuery.Where(e => EF.Property<Point>(e, "Location").IsWithinDistance(userLocation, radiusInMeters));

        eventsQuery = eventsQuery.Where(e => e.Status == SportEventStatus.Scheduled);

        if (!string.IsNullOrWhiteSpace(request.Sport))
        {
            eventsQuery = eventsQuery.Where(e => e.Sport == request.Sport);
        }

        if (request.FromDate.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.Date >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.Date <= request.ToDate.Value);
        }

        var joinedQuery =
            from e in eventsQuery

            join v in _context.Venues.AsNoTracking() on e.VenueId equals v.Id into vGroup
            from venue in vGroup.DefaultIfEmpty()

            join c in _context.Cities.AsNoTracking()
                on (venue != null! ? venue.CityId : e.CityId) equals c.Id into cGroup
            from city in cGroup.DefaultIfEmpty()

            let distance = EF.Property<Point>(e, "Location").Distance(userLocation)

            orderby distance

            select new SportEventDto(
                e.Id.Value,
                e.Name,
                e.Sport,
                e.Date,
                e.Coordinates.Latitude,
                e.Coordinates.Longitude,
                distance,
                venue != null! ? venue.Name : null,
                city != null! ? city.Name : null,
                city != null! ? city.Country : null
            );

        return await joinedQuery.ToListAsync(cancellationToken);
    }
}
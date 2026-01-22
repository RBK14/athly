using Athly.SportEvents.Application.Interfaces;
using Athly.SportEvents.Application.SportEvents.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Athly.SportEvents.Application.SportEvents.Queries.GetNearbySportEvents
{
    public class GetNearbySportEventsHandler(ISportEventsContext context) : IRequestHandler<GetNearbySportEventsQuery, IEnumerable<SportEventDto>>
    {
        private readonly ISportEventsContext _context = context;
        private readonly GeometryFactory _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        public async Task<IEnumerable<SportEventDto>> Handle(GetNearbySportEventsQuery request, CancellationToken cancellationToken)
        {
            var userLocation = _geometryFactory.CreatePoint(new Coordinate(request.UserLongitude, request.UserLatitude));
            double radiusInMeters = request.RadiusInKm * 1000;

            var query = _context.SportEvents.AsNoTracking();

            query = query.Where(e => EF.Property<Point>(e, "Location").IsWithinDistance(userLocation, radiusInMeters));

            if (!string.IsNullOrWhiteSpace(request.Sport))
            {
                query = query.Where(e => e.Sport == request.Sport);
            }

            if (request.FromDate.HasValue)
            {
                query = query.Where(e => e.Date >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                query = query.Where(e => e.Date <= request.ToDate.Value);
            }

            return await query
                .OrderBy(e => EF.Property<Point>(e, "Location").Distance(userLocation))
                .Select(e => new SportEventDto(
                    e.Id.Value,
                    e.Name,
                    e.Sport,
                    e.Date,
                    e.Status.ToString(),
                    e.Coordinates.Latitude,
                    e.Coordinates.Longitude,
                    EF.Property<Point>(e, "Location").Distance(userLocation),
                    e.VenueName,
                    e.City,
                    e.Country,
                    e.League,
                    e.Season,
                    e.Description,
                    e.ImageUrl
                ))
                .ToListAsync(cancellationToken);
        }
    }
}

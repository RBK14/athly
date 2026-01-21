using Athly.SportEvents.Application.Interfaces;
using Athly.SportEvents.Application.SportEvents.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Athly.SportEvents.Application.SportEvents.Queries.GetNearbySportEvents
{
    public class GetNearbySportEventsHandler(ISportEventsContext context) : IRequestHandler<GetNearbySportEventsQuery, IEnumerable<SportEventDto>>
    {
        private readonly ISportEventsContext _context = context;
        private readonly GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        public async Task<IEnumerable<SportEventDto>> Handle(GetNearbySportEventsQuery request, CancellationToken cancellationToken)
        {
            var userLocation = geometryFactory.CreatePoint(new Coordinate(request.UserLongitude, request.UserLatitude));
            double radiusInMeters = request.RadiusInKm * 1000;

            var dbQuery = _context.SportEvents.AsNoTracking();

            dbQuery = dbQuery.Where(e => EF.Property<Point>(e, "Location").IsWithinDistance(userLocation, radiusInMeters));

            if (request.FromDate.HasValue)
            {
                dbQuery = dbQuery.Where(e => e.Date >= request.FromDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Sport))
            {
                dbQuery = dbQuery.Where(e => e.Sport == request.Sport);
            }

            var dtos = await dbQuery
                .Select(e => new SportEventDto(
                    e.Id.Value,
                    e.Name,
                    e.Sport,
                    e.Date,
                    e.Status.ToString(),
                    e.EventCoordinates.Longitude,
                    e.EventCoordinates.Latitude,
                    EF.Property<Point>(e, "Location").Distance(userLocation),
                    e.VenueName,
                    e.City,
                    e.Country,
                    e.League,
                    e.Season,
                    e.Description,
                    e.ImageUrl))
                .OrderBy(e => e.DistanceInMeters)
                .ToListAsync(cancellationToken);

            return dtos;
        }
    }
}

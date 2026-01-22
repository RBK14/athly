using Athly.SportEvents.Application.SportEvents.Common;
using Athly.SportEvents.Application.SportEvents.Queries.GetNearbySportEvents;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Athly.SportEvents.API.Apis
{
    public static class SportEventsApi
    {
        public static RouteGroupBuilder MapSportEventsApi(this IEndpointRouteBuilder app)
        {
            var api = app.MapGroup("api/sport-events");

            api.MapGet("/", GetNearbySportEvents)
                .WithName("GetNearbySportEvents")
                .WithOpenApi();

            return api;
        }

        public static async Task<Results<Ok<IEnumerable<SportEventDto>>, BadRequest<string>, ProblemHttpResult>> GetNearbySportEvents(
        [AsParameters] SportEventsServices services,
        [AsParameters] GetNearbyEventsRequest request
        )
        {
            services.Logger.LogInformation(
                "Fetching events. Lat: {Lat}, Lon: {Lon}, Radius: {Radius}, Sport: {Sport}, From: {From}, To: {To} ",
                request.Lat, request.Lon, request.Radius, request.Sport, request.From, request.To);

            // TODO: Utworzyć walidator dla Query
            if (request.Radius <= 0 || request.Radius > 100)
            {
                return TypedResults.BadRequest("Radius must be between 0 and 100 km.");
            }

            var query = new GetNearbySportEventsQuery(
                request.Lat,
                request.Lon,
                request.Radius,
                request.Sport,
                request.From,
                request.To);

            var result = await services.Mediator.Send(query);

            return TypedResults.Ok(result);
        }
    }

    public record GetNearbyEventsRequest(
        double Lat,
        double Lon,
        double Radius = 20,
        string? Sport = null,
        DateTimeOffset? From = null,
        DateTimeOffset? To = null);
}



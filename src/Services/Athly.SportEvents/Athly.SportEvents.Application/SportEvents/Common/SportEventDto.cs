namespace Athly.SportEvents.Application.SportEvents.Common
{
    public record SportEventDto(
        Guid Id,
        string Name,
        string Sport,
        DateTimeOffset Date,
        string Status,
        double Latitude,
        double Longitude,
        double DistanceInMeters,
        string? VenueName,
        string? City,
        string? Country,
        string? League,
        string? Season,
        string? Description,
        string? ImageUrl);
}

using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Cities;
using Athly.SportEvents.Domain.Common.Exceptions;
using Athly.SportEvents.Domain.Common.ValueObjects;
using Athly.SportEvents.Domain.Venues;

namespace Athly.SportEvents.Domain.SportEvents;

public class SportEvent : AggregateRoot<SportEventId>
{
    public string ExternalId { get; init; }

    public string Name { get; private set; }
    public string Sport { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public SportEventStatus Status { get; private set; }
    public Coordinates Coordinates { get; private set; }

    public VenueId? VenueId { get; private set; }
    public CityId? CityId { get; private set; }

    public string? League { get; private set; }
    public string? Season { get; private set; }
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }

    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    private SportEvent(
        SportEventId id,
        string externalId,
        string name,
        string sport,
        DateTimeOffset date,
        Coordinates coordinates,
        VenueId? venueId,
        CityId? cityId) : base(id)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new DomainException("External ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Event name cannot be empty.");

        if (string.IsNullOrWhiteSpace(sport))
            throw new DomainException("Sport type is required.");

        if (date == default)
            throw new DomainException("Date must be set to a valid value.");

        ExternalId = externalId;
        Name = name;
        Sport = sport;
        Date = date;
        Coordinates = coordinates;
        Status = SportEventStatus.Scheduled;
        VenueId = venueId;
        CityId = cityId;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = null;
    }

    public static SportEvent CreateAtVenue(
        string externalId,
        string name,
        string sport,
        DateTimeOffset date,
        Coordinates venueCoordinates,
        VenueId? venueId,
        CityId cityId)
    {
        return new SportEvent(
            SportEventId.New(),
            externalId,
            name,
            sport,
            date,
            venueCoordinates,
            venueId,
            cityId);
    }

    public static SportEvent CreateInCity(
        string externalId,
        string name,
        string sport,
        DateTimeOffset date,
        Coordinates cityCoordinates,
        CityId cityId)
    {
        return new SportEvent(
            SportEventId.New(),
            externalId,
            name,
            sport,
            date,
            cityCoordinates,
            null,
            cityId);
    }

    public void SetLeagueDetails(string? league, string? season)
    {
        League = league;
        Season = season;
    }

    public void SetContent(string? description, string? imageUrl)
    {
        Description = description;
        ImageUrl = imageUrl;
    }

    // TODO: Dorobić metody do edycji

    public void UpdateStatus(SportEventStatus status, DateTimeOffset? newDate = null)
    {
        Status = status;
        if (newDate.HasValue)
        {
            Date = newDate.Value;
        }

        UpdatedAt = DateTimeOffset.UtcNow;
    }

#pragma warning disable CS8618
    private SportEvent() { }
#pragma warning restore CS8618
}
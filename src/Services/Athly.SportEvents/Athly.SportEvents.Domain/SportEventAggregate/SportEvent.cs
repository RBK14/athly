using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;
using Athly.SportEvents.Domain.Common.ValueObjects;
using Athly.SportEvents.Domain.SportEventAggregate.Enums;
using Athly.SportEvents.Domain.SportEventAggregate.ValueObjects;

namespace Athly.SportEvents.Domain.SportEventAggregate;

public class SportEvent : AggregateRoot<SportEventId>
{
    public string ExternalId { get; private set; }

    public string Name { get; private set; }
    public string Sport { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public SportEventStatus Status { get; private set; }
    public Coordinates Coordinates { get; private set; }

    public string? VenueName { get; private set; }
    public string? City { get; private set; }
    public string? Country { get; private set; }
    public string? League { get; private set; }
    public string? Season { get; private set; }

    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }

    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private SportEvent(
        SportEventId id,
        string externalId,
        string name,
        string sport,
        DateTimeOffset date,
        Coordinates coordinates) : base(id)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new DomainException("ExternalId cannot be empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Event Name cannot be empty.");

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
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public static SportEvent Create(
        string externalId,
        string name,
        string sport,
        DateTimeOffset date,
        Coordinates coordinates)
    {
        return new SportEvent(SportEventId.New(), externalId, name, sport, date, coordinates);
    }

    public void SetExternalId(string externalId)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new DomainException("ExternalId cannot be empty.");

        ExternalId = externalId;

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void SetDetails(string? league, string? season, string? venueName, string? city, string? country)
    {
        League = league;
        Season = season;
        VenueName = venueName;
        City = city;
        Country = country;

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void SetContent(string? description, string? imageUrl)
    {
        Description = description;
        ImageUrl = imageUrl;

        UpdatedAt = DateTimeOffset.UtcNow;
    }

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
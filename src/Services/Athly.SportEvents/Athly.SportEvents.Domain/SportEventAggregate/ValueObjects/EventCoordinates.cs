using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;

namespace Athly.SportEvents.Domain.SportEventAggregate.ValueObjects;

public class EventCoordinates : ValueObject
{
    public double Longitude { get; }    // X
    public double Latitude { get; }     // Y

    private EventCoordinates(double longitude, double latitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new DomainException("Latitude must be between -90 and 90 degrees.");

        if (longitude < -180 || longitude > 180)
            throw new DomainException("Longitude must be between -180 and 180 degrees.");

        Longitude = longitude;
        Latitude = latitude;
    }

    public static EventCoordinates Create(double longitude, double latitude)
    {
        return new EventCoordinates(longitude, latitude);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Longitude;
        yield return Latitude;
    }

#pragma warning disable CS8618
    private EventCoordinates() { }
#pragma warning restore CS8618
}
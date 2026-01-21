using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;

namespace Athly.SportEvents.Domain.SportEventAggregate.ValueObjects;

public class Coordinates : ValueObject
{
    public double Latitude { get; }
    public double Longitude { get; }

    private Coordinates(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new DomainException("Latitude must be between -90 and 90 degrees.");

        if (longitude < -180 || longitude > 180)
            throw new DomainException("Longitude must be between -180 and 180 degrees.");

        Latitude = latitude;
        Longitude = longitude;
    }

    public static Coordinates Create(double latitude, double longitude)
    {
        return new Coordinates(latitude, longitude);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }

    public override string ToString() => $"{Latitude}, {Longitude}";

#pragma warning disable CS8618
    private Coordinates() { }
#pragma warning restore CS8618
}
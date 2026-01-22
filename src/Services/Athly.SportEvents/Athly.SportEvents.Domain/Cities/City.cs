using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;
using Athly.SportEvents.Domain.Common.ValueObjects;

namespace Athly.SportEvents.Domain.Cities
{
    public class City : AggregateRoot<CityId>
    {
        public string Name { get; private set; }
        public string Country { get; private set; }
        public Coordinates Coordinates { get; private set; }

        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        private City (
            CityId id,
            string name,
            string country,
            Coordinates coordinates) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("City name cannot be empty.");

            if (string.IsNullOrWhiteSpace(country))
                throw new DomainException("Country cannot be empty.");

            Name = name;
            Country = country;
            Coordinates = coordinates;
            CreatedAt = DateTimeOffset.Now;
            UpdatedAt = null;
        }

        public static City Create(
            string name,
            string country,
            Coordinates coordinates)
        {
            return new City(
                CityId.New(),
                name,
                country,
                coordinates);
        }

        // TODO: Dorobić metody do edycji

#pragma warning disable CS8618
        private City() { }
#pragma warning restore CS8618
    }
}

using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Cities;
using Athly.SportEvents.Domain.Common.Exceptions;
using Athly.SportEvents.Domain.Common.ValueObjects;

namespace Athly.SportEvents.Domain.Venues
{
    public class Venue : AggregateRoot<VenueId>
    {
        public string ExternalId { get; init; }
        public string Name { get; private set; }
        public Coordinates Coordinates { get; private set; }

        public CityId CityId { get; private set; }

        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        private Venue(
            VenueId id,
            string externalId,
            string name,
            Coordinates coordinates,
            CityId cityId) : base(id)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new DomainException("External ID is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Venue name is required.");

            ExternalId = externalId;
            Name = name;
            Coordinates = coordinates;
            CityId = cityId;
            CreatedAt = DateTimeOffset.Now;
            UpdatedAt = null;
        }

        public static Venue Create(
            string externalId,
            string name,
            Coordinates coordinates,
            CityId cityId)
        {
            return new Venue(VenueId.New(),
                externalId,
                name,
                coordinates,
                cityId);
        }

        // TODO: Dorobić metody do edycji

#pragma warning disable CS8618
        private Venue() { }
#pragma warning restore CS8618
    }
}

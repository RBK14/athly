using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;

namespace Athly.SportEvents.Domain.Venues
{
    public class VenueId : ValueObject
    {
        public Guid Value { get; init; }

        private VenueId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Venue ID cannot be empty");

            Value = value;
        }

        public static VenueId New() => new(Guid.NewGuid());
        public static VenueId Of(Guid value) => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

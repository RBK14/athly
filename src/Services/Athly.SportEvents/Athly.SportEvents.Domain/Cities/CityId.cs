using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;

namespace Athly.SportEvents.Domain.Cities
{
    public class CityId : ValueObject
    {
        public Guid Value { get; init; }

        private CityId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("City ID cannot be empty");

            Value = value;
        }

        public static CityId New() => new(Guid.NewGuid());
        public static CityId Of(Guid value) => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

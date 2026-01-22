using Athly.BuildingBlocks.Domain;
using Athly.SportEvents.Domain.Common.Exceptions;

namespace Athly.SportEvents.Domain.SportEvents;

public class SportEventId : ValueObject
{
    public Guid Value { get; init; }

    private SportEventId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Event ID cannot be empty");

        Value = value;
    }

    public static SportEventId New() => new(Guid.NewGuid());
    public static SportEventId Of(Guid value) => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
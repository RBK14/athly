using Athly.BuildingBlocks.Domain;

namespace Athly.SportEvents.Domain.SportEventAggregate.ValueObjects;

public class SportEventId : ValueObject
{
    public Guid Value { get; }

    private SportEventId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Event ID cannot be empty", nameof(value));

        Value = value;
    }

    public static SportEventId New() => new(Guid.NewGuid());
    public static SportEventId Of(Guid value) => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
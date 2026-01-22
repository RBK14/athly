using Athly.SportEvents.Domain.Common.Exceptions;

namespace Athly.SportEvents.Domain.SportEvents;

public enum SportEventStatus
{
    Scheduled,
    Cancelled,
    Completed
}

public static class SportEventStatusExtensions
{
    public static bool TryParse(string? value, out SportEventStatus type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            type = default;
            return false;
        }

        return Enum.TryParse(value, ignoreCase: true, out type)
               && Enum.IsDefined(typeof(SportEventStatus), type);
    }

    public static SportEventStatus Parse(string value)
    {
        if (!TryParse(value, out var type))
            throw new DomainException($"Invalid accommodation type: {value}");

        return type;
    }

    public static bool IsValidStatus(string? value)
    {
        return TryParse(value, out _);
    }
}
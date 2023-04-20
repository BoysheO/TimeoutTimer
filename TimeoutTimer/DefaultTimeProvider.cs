using System;

namespace BoysheO.TimeoutTimerSystem;

public sealed class DefaultTimeProvider : ITimeProvider
{
    public static readonly DefaultTimeProvider Instance = new();
    public DateTimeOffset GetNow()
    {
        return DateTimeOffset.UtcNow;
    }
}
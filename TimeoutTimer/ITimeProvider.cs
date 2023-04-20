using System;

namespace BoysheO.TimeoutTimerSystem;
/// <summary>
/// You must make sure the time the implement provided can be work in infinite loop,
/// or cause the TimeoutTimer can not become timeout in the infinite loop
/// </summary>
public interface ITimeProvider
{
    DateTimeOffset GetNow();
}
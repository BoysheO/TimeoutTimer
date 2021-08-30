using System;

namespace susi.util.toolkit
{
    public readonly struct TimeoutTimer
    {
        public TimeoutTimer(TimeSpan timeout)
        {
            CreatTime = DateTimeOffset.UtcNow;
            Timeout = timeout;
        }

        public TimeoutTimer(float timeoutSec) : this(TimeSpan.FromSeconds(timeoutSec))
        {
        }

        public TimeSpan Timeout { get; }
        public DateTimeOffset CreatTime { get; }

        public void ThrowIfTimeout()
        {
            if (DateTimeOffset.UtcNow > CreatTime + Timeout)
                throw new TimeoutException($"time out in {Timeout.TotalSeconds}s");
        }
    }
}
using System;
using System.Runtime.CompilerServices;

namespace com.susi.util.toolkit
{
    public readonly struct TimeoutTimer
    {
        public TimeoutTimer(TimeSpan timeout)
        {
            CreationTime = DateTimeOffset.UtcNow;
            Timeout = timeout;
        }

        public TimeoutTimer(float timeoutSec) : this(TimeSpan.FromSeconds(timeoutSec))
        {
        }

        public TimeSpan Timeout { get; }
        public DateTimeOffset CreationTime { get; }

        public bool IsTimeout => DateTimeOffset.UtcNow > CreationTime + Timeout;

        public void ThrowIfTimeout(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        )
        {
            ThrowIfTimeout(new TimeoutException($"time out in {Timeout.TotalSeconds}s ({memberName} at {sourceFilePath}:{sourceLineNumber})"));
        }

        public void ThrowIfTimeout(TimeoutException ex)
        {
            if (IsTimeout) throw ex;
        }
    }
}
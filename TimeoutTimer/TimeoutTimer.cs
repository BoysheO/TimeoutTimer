using System;
using System.Runtime.CompilerServices;

namespace BoysheO.TimeoutTimerSystem
{
    public readonly struct TimeoutTimer
    {
        public readonly ITimeProvider TimeProvider;

        public TimeoutTimer(TimeSpan timeout, ITimeProvider timeProvider)
        {
            Timeout = timeout;
            TimeProvider = timeProvider;
            CreationTime = TimeProvider.GetNow();
        }

        public TimeoutTimer(TimeSpan timeout) : this(timeout, DefaultTimeProvider.Instance)
        {
        }

        public TimeoutTimer(float timeoutSec) : this(TimeSpan.FromSeconds(timeoutSec))
        {
        }

        /// <summary>
        /// 超时时长
        /// </summary>
        public readonly TimeSpan Timeout;

        /// <summary>
        /// TimeoutTimer创建时刻
        /// </summary>
        public readonly DateTimeOffset CreationTime;

        /// <summary>
        /// 剩余时长
        /// </summary>
        public TimeSpan RemainingTime => Timeout - (TimeProvider.GetNow() - CreationTime);

        /// <summary>
        /// 剩余时长
        /// </summary>
        public double RemainingSec => RemainingTime.TotalSeconds;

        public bool IsTimeout => TimeProvider.GetNow() > CreationTime + Timeout;

        public void ThrowIfTimeout(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        )
        {
            ThrowIfTimeout(
                $"time out in {Timeout.TotalSeconds}s ({memberName} at {sourceFilePath}:{sourceLineNumber})");
        }

        public void ThrowIfTimeout(string exMsg)
        {
            if (IsTimeout) throw new TimeoutException(exMsg);
        }
    }
}
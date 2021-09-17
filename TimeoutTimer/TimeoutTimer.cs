using System;
using System.Runtime.CompilerServices;

namespace com.susi.toolkit
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

        /// <summary>
        /// 超时时长
        /// </summary>
        public TimeSpan Timeout { get; }
        /// <summary>
        /// TimeoutTimer创建时刻
        /// </summary>
        public DateTimeOffset CreationTime { get; }
        /// <summary>
        /// 剩余时长
        /// </summary>
        public TimeSpan RemainingTime => Timeout - (DateTimeOffset.UtcNow - CreationTime);
        /// <summary>
        /// 剩余时长
        /// </summary>
        public float RemainingSec => (float)RemainingTime.TotalSeconds;

        public bool IsTimeout => DateTimeOffset.UtcNow > CreationTime + Timeout;

        public void ThrowIfTimeout(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        )
        {
            ThrowIfTimeout($"time out in {Timeout.TotalSeconds}s ({memberName} at {sourceFilePath}:{sourceLineNumber})");
        }

        public void ThrowIfTimeout(string exMsg)
        {
            if (IsTimeout) throw new TimeoutException(exMsg);
        }
    }
}
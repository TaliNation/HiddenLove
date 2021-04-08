using System;

namespace HiddenLove.Shared.Modules
{
    public static class TimeCalculator
    {
        public static int MillisecondsUntil(DateTime dateTime)
        {
            TimeSpan time = dateTime - DateTime.Now;
            return (int)time.TotalMilliseconds;
        }
    }
}
namespace HiddenLove.Shared.Modules
{
    public static class TimeCalculator
    {
        public static int MillisecondsUntil(DateTime time)
        {
            TimeSpan time = dateTime - DateTime.Now;
            return (int)time.TotalMilliseconds;
        }
    }
}
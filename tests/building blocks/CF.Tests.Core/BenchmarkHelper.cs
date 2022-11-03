using System.Diagnostics;

namespace CF.Tests.Core
{
    public class BenchmarkHelper
    {
        public static TimeSpan MeasureTime(Action actionToMeasure)
        {
            Stopwatch cronometer = Stopwatch.StartNew();
            actionToMeasure();
            cronometer.Stop();
            return cronometer.Elapsed;
        }
    }
}
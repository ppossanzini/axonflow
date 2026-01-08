using System;

namespace Hikyaku.Kaido.Stats
{
    public class PerformanceStat
    {
        public PerformanceStat(Type t)
        {
            TypeName = t.FullName;
        }

        public string TypeName { get; private set; }
        public long ServedLocally { get; internal set; }
        public long RemoteRequest { get; internal set; }
        public long Errors { get; internal set; }
        public long TotalExecutionTime { get; internal set; }

        public long TotalRequests
        {
            get { return ServedLocally + RemoteRequest; }
        }

        public long SuccessfulRequests
        {
            get { return ServedLocally + RemoteRequest - Errors; }
        }

        public TimeSpan AverageExecutionTime
        {
            // ReSharper disable once PossibleLossOfFraction
            get { return SuccessfulRequests > 0 ? TimeSpan.FromMilliseconds(TotalExecutionTime / SuccessfulRequests) : TimeSpan.Zero; }
        }
    }
}
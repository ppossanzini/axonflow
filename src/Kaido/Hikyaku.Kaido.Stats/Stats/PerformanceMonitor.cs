using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hikyaku.Kaido.Stats
{
    public class PerformanceMonitor : IPerformanceMonitor, IDisposable
    {
        private static readonly Dictionary<Type, PerformanceStat> stats = new Dictionary<Type, PerformanceStat>();

        private readonly Stopwatch _watch = new Stopwatch();

        public IEnumerable<PerformanceStat> GetStats
        {
            get { return stats.Values; }
        }

        private PerformanceStat GetPerformanceInfo(Type t)
        {
            lock (stats)
            {
                if (stats.TryGetValue(t, out var item)) return item;
                item = new PerformanceStat(t);
                stats.Add(t, item);
                return item;
            }
        }

        public void NewLocalRequest(Type type)
        {
            var s = GetPerformanceInfo(type);
            lock (s) s.ServedLocally++;
            _watch.Start();
        }

        public void NewRemoteRequest(Type type)
        {
            var s = GetPerformanceInfo(type);
            lock (s) s.RemoteRequest++;
            _watch.Start();
        }

        public void SuccessfullyCompleted(Type type)
        {
            _watch.Stop();
            var s = GetPerformanceInfo(type);
            lock (s) s.TotalExecutionTime += _watch.ElapsedMilliseconds;
        }

        public void CompletedWithExceptions(Type type)
        {
            _watch.Stop();
            var s = GetPerformanceInfo(type);
            lock (s) s.Errors++;
        }

        public void Dispose()
        {
            _watch.Reset();
        }
    }
}
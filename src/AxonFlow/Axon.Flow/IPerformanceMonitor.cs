using System;
using System.Collections.Generic;
using Axon.Flow.Stats;

namespace Axon.Flow
{
    public interface IPerformanceMonitor
    {
        IEnumerable<PerformanceStat> GetStats { get; }
        void NewLocalRequest(Type type);
        void NewRemoteRequest(Type type);
        void SuccessfullyCompleted(Type type);
        void CompletedWithExceptions(Type type);
    }
}
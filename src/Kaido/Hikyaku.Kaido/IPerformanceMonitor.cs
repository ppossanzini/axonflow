using System;
using System.Collections.Generic;

namespace Hikyaku.Kaido
{
    public interface IPerformanceMonitor
    {
        void NewLocalRequest(Type type);
        void NewRemoteRequest(Type type);
        void SuccessfullyCompleted(Type type);
        void CompletedWithExceptions(Type type);
    }
}
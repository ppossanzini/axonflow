using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Axon.Flow.Stats.Controllers
{
    [ApiController]
    [Route("_axon/stats")]
    public class AxonStatsController : ControllerBase
    {
        private readonly IPerformanceMonitor _performanceMonitor;

        public AxonStatsController(IPerformanceMonitor performanceMonitor)
        {
            this._performanceMonitor = performanceMonitor;
        }

        [HttpGet]
        public IEnumerable<PerformanceStat> GetRequestsStats(StatsOrderBy orderBy = StatsOrderBy.Name)
        {
            switch (orderBy)
            {
                case StatsOrderBy.AverageTime: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenBy(i => i.AverageExecutionTime);
                case StatsOrderBy.AverageTimeDesc: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenByDescending(i => i.AverageExecutionTime);
                case StatsOrderBy.TotalRequest: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenBy(i => i.TotalRequests);
                case StatsOrderBy.TotalRequestDesc: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenByDescending(i => i.TotalRequests);
                case StatsOrderBy.LocalRequest: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenBy(i => i.ServedLocally);
                case StatsOrderBy.LocalRequestDesc: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenByDescending(i => i.ServedLocally);
                case StatsOrderBy.RemoteRequest: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenBy(i => i.RemoteRequest);
                case StatsOrderBy.RemoteRequestDesc: return _performanceMonitor.GetStats.OrderBy(i => i.TypeName).ThenByDescending(i => i.RemoteRequest);
                case StatsOrderBy.Name:
                default:
                    return _performanceMonitor.GetStats.OrderBy(i => i.TypeName);
            }
        }
    }

    public enum StatsOrderBy
    {
        Name,
        TotalRequest,
        LocalRequest,
        RemoteRequest,
        AverageTime,
        TotalRequestDesc,
        LocalRequestDesc,
        RemoteRequestDesc,
        AverageTimeDesc
    }
}
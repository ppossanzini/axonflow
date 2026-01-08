using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Hikyaku.Kaido.Stats.Controllers
{
  [ApiController]
  [Route("_kaido/stats")]
  public class KaidoStatsController : ControllerBase
  {
    private readonly PerformanceMonitor _performanceMonitor;
    private readonly IRouter _router;

    public KaidoStatsController(IPerformanceMonitor performanceMonitor, IRouter router)
    {
      _performanceMonitor = performanceMonitor as PerformanceMonitor;
      _router = router;
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

    [HttpGet("/unused")]
    public IEnumerable<string> GetUnusedTypes()
    {
      return _router.GetLocalRequestsTypes().Where(t => _performanceMonitor.GetStats.All(i => i.TypeName != t.FullName)).Select(i => i.FullName);
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
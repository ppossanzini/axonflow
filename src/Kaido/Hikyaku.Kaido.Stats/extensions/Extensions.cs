using Hikyaku.Kaido.Stats;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class Extensions
  {
    public static IServiceCollection AddKaidoPerformanceMonitor(this IServiceCollection services)
    {
      services.AddTransient<global::Hikyaku.Kaido.IPerformanceMonitor, PerformanceMonitor>();
        
      return services;
    }
  }
}
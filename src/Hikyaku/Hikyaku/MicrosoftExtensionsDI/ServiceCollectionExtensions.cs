using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Hikyaku;
using Hikyaku.Pipeline;
using Hikyaku.Registration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions to scan for Hikyaku handlers and registers them.
/// - Scans for any handler interface implementations and registers them as <see cref="ServiceLifetime.Transient"/>
/// - Scans for any <see cref="IRequestPreProcessor{TRequest}"/> and <see cref="IRequestPostProcessor{TRequest,TResponse}"/> implementations and registers them as transient instances
/// Registers <see cref="IHikyaku"/> as a transient instance
/// After calling AddMediatR you can use the container to resolve an <see cref="IHikyaku"/> instance.
/// This does not scan for any <see cref="IPipelineBehavior{TRequest,TResponse}"/> instances including <see cref="RequestPreProcessorBehavior{TRequest,TResponse}"/> and <see cref="RequestPreProcessorBehavior{TRequest,TResponse}"/>.
/// To register behaviors, use the <see cref="ServiceCollectionServiceExtensions.AddTransient(IServiceCollection,Type,Type)"/> with the open generic or closed generic types.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Registers handlers and orchestrator types from the specified assemblies
  /// </summary>
  /// <param name="services">Service collection</param>
  /// <param name="configuration">The action used to configure the options</param>
  /// <returns>Service collection</returns>
  public static IServiceCollection AddMediatR(this IServiceCollection services,
    Action<HikyakuServiceConfiguration> configuration)
  {
    return AddHikyaku(services, configuration);
  }

  /// <summary>
  /// Registers handlers and orchestrator types from the specified assemblies
  /// </summary>
  /// <param name="services">Service collection</param>
  /// <param name="configuration">Configuration options</param>
  /// <returns>Service collection</returns>
  public static IServiceCollection AddMediatR(this IServiceCollection services,
    HikyakuServiceConfiguration configuration)
  {
    return AddHikyaku(services, configuration);
  }
  
  
  /// <summary>
  /// Registers handlers and orchestrator types from the specified assemblies
  /// </summary>
  /// <param name="services">Service collection</param>
  /// <param name="configuration">The action used to configure the options</param>
  /// <returns>Service collection</returns>
  public static IServiceCollection AddHikyaku(this IServiceCollection services,
    Action<HikyakuServiceConfiguration> configuration)
  {
    var serviceConfig = new HikyakuServiceConfiguration();
    configuration.Invoke(serviceConfig);
    return services.AddHikyaku(serviceConfig);
  }
  
  
  /// <summary>
  /// Registers handlers and orchestrator types from the specified assemblies
  /// </summary>
  /// <param name="services">Service collection</param>
  /// <param name="configuration">Configuration options</param>
  /// <returns>Service collection</returns>
  public static IServiceCollection AddHikyaku(this IServiceCollection services,
    HikyakuServiceConfiguration configuration)
  {
    if (!configuration.AssembliesToRegister.Any())
    {
      throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
    }

    ServiceRegistrar.SetGenericRequestHandlerRegistrationLimitations(configuration);

    ServiceRegistrar.AddHikyakuClassesWithTimeout(services, configuration);

    ServiceRegistrar.AddRequiredServices(services, configuration);

    return services;
  }
}
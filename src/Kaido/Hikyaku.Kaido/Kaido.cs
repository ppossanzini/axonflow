using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hikyaku.Kaido;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hikyaku.Kaido
{
  /// OrchestratorGateway class is a subclass of Orchestrator that adds additional functionality for remote request arbitration.
  /// /
  public class Kaido : global::Hikyaku.Hikyaku
  { 
    private readonly IRouter _router;
    private readonly ILogger<Kaido> _logger;
    private bool _allowRemoteRequest = true;
    

    public Kaido(IServiceProvider serviceProvider, IRouter router, ILogger<Kaido> logger) : base(serviceProvider)
    {
      this._router = router;
      this._logger = logger;
    }

    /// <summary>
    /// Stops the propagation of remote requests.
    /// </summary>
    public void StopPropagating()
    {
      _allowRemoteRequest = false;
    }

    /// <summary>
    /// Resets the propagating state to allow remote requests.
    /// </summary>
    public void ResetPropagating()
    {
      _allowRemoteRequest = true;
    }

    /// <summary>
    /// Publishes the given notification by invoking the registered notification handlers.
    /// </summary>
    /// <param name="handlerExecutors">The notification handler executors.</param>
    /// <param name="notification">The notification to publish.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task PublishCore(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification,
      CancellationToken cancellationToken)
    {
      var not = notification;
      
      try
      {
        if (_allowRemoteRequest)
        {
          await _router.SendRemoteNotification(not);
        }
        else
        {
            await base.PublishCore(handlerExecutors, not, cancellationToken);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        throw;
      }
    }
  }
}
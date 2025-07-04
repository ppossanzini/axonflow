using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AxonFlow;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AxonFlow
{
  /// OrchestratorGateway class is a subclass of Orchestrator that adds additional functionality for remote request arbitration.
  /// /
  public class AxonFlow : global::AxonFlow.Axon
  {
    private readonly IRouter _router;
    private readonly ILogger<AxonFlow> _logger;
    private bool _allowRemoteRequest = true;

    public AxonFlow(IServiceProvider serviceProvider, IRouter router, ILogger<AxonFlow> logger) : base(serviceProvider)
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
      string queueName = null;
      if (typeof(IExplicitQueue).IsAssignableFrom(notification.GetType()))
      {
        not = (notification as ExplicitQueueNotification<INotification>).Message;
        queueName = (notification as IExplicitQueue).QueueName;
      }

      try
      {
        if (_allowRemoteRequest)
        {
          await _router.SendRemoteNotification(not, queueName);
        }
        else
          await base.PublishCore(handlerExecutors, not, cancellationToken);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        throw;
      }
    }
  }
}
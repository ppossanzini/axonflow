using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.NotificationPublishers;

/// <summary>
/// Uses Task.WhenAll with the list of Handler tasks:
/// <code>
/// var tasks = handlers
///                .Select(handler => handler.Handle(notification, cancellationToken))
///                .ToList();
/// 
/// return Task.WhenAll(tasks);
/// </code>
/// </summary>
public class TaskWhenAllPublisher : INotificationPublisher
{
  /// <summary>
  /// Publishes a notification to all provided handler executors by invoking their callback methods
  /// using Task.WhenAll to execute them concurrently.
  /// </summary>
  /// <param name="handlerExecutors">A collection of handler executors that process the notification.</param>
  /// <param name="notification">The notification to be handled by the executors.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <returns>A task that represents the asynchronous operation of executing all handler callbacks.</returns>
  public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        var tasks = handlerExecutors
            .Select(handler => handler.HandlerCallback(notification, cancellationToken))
            .ToArray();

        return Task.WhenAll(tasks);
    }
}
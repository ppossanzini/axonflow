using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Axon.NotificationPublishers;

/// <summary>
/// Awaits each notification handler in a single foreach loop:
/// <code>
/// foreach (var handler in handlers) {
///     await handler(notification, cancellationToken);
/// }
/// </code>
/// </summary>
public class ForeachAwaitPublisher : MediatR.INotificationPublisher
{
  /// <summary>
  /// Publishes a notification by invoking each handler sequentially using await.
  /// </summary>
  /// <param name="handlerExecutors">A collection of notification handler executors to be invoked.</param>
  /// <param name="notification">The notification instance to be published.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <returns>A task that represents the asynchronous operation of invoking all handlers sequentially.</returns>
  public async Task Publish(IEnumerable<MediatR.NotificationHandlerExecutor> handlerExecutors, MediatR.INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in handlerExecutors)
        {
            await handler.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
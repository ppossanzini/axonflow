using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;


namespace MediatR;

/// <summary>
/// Represents a contract for publishing notifications to a collection of registered notification handlers.
/// </summary>
public interface INotificationPublisher
{
  /// <summary>
  /// Publishes a notification by executing the provided collection of notification handler executors.
  /// </summary>
  /// <param name="handlerExecutors">A collection of <see cref="NotificationHandlerExecutor"/> objects, each representing a handler instance and its associated callback for processing the notification.</param>
  /// <param name="notification">The notification object to be published to the handlers.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete, enabling cooperative cancellation.</param>
  /// <returns>A <see cref="Task"/> that represents the asynchronous operation of invoking all handlers.</returns>
  Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, MediatR.INotification notification,
        CancellationToken cancellationToken);
}
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;


namespace Hikyaku;

/// <summary>
/// Represents a contract for publishing notifications to a collection of registered notification handlers.
/// </summary>
public interface INotificationPublisher 
{
  Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification,
    CancellationToken cancellationToken);

}
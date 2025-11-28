using System.Threading;
using System.Threading.Tasks;



namespace Axon
{
  /// <summary>
  /// Defines a handler for a notification
  /// </summary>
  /// <typeparam name="TNotification">The type of notification being handled</typeparam>
  public interface INotificationHandler<in TNotification> : MediatR.INotificationHandler<TNotification>
    where TNotification : MediatR.INotification
  {
  }

  /// <summary>
  /// Wrapper class for a synchronous notification handler
  /// </summary>
  /// <typeparam name="TNotification">The notification type</typeparam>
  public abstract class NotificationHandler<TNotification> : MediatR.NotificationHandler<TNotification>
    where TNotification : MediatR.INotification
  {
  }
}
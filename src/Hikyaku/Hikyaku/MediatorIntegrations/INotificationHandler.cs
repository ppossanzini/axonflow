using System.Threading;
using System.Threading.Tasks;



namespace MediatR
{
  /// <summary>
  /// Defines a handler for a notification
  /// </summary>
  /// <typeparam name="TNotification">The type of notification being handled</typeparam>
  public interface INotificationHandler<in TNotification>: Hikyaku.INotificationHandler<TNotification>
    where TNotification : INotification
  {

  }

  /// <summary>
  /// Wrapper class for a synchronous notification handler
  /// </summary>
  /// <typeparam name="TNotification">The notification type</typeparam>
  public abstract class NotificationHandler<TNotification> : Hikyaku.NotificationHandler<TNotification>
    where TNotification : INotification
  {

  }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;


namespace Axon;

/// <summary>
/// Represents a contract for publishing notifications to a collection of registered notification handlers.
/// </summary>
public interface INotificationPublisher : MediatR.INotificationPublisher
{

}
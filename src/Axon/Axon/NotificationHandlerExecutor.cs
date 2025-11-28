using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Axon;

/// <summary>
/// Represents an executor for handling notifications, containing a specific handler instance
/// and a callback function to process the notification asynchronously.
/// </summary>
/// <param name="HandlerInstance">
/// The instance of the handler responsible for processing the notification.
/// </param>
/// <param name="HandlerCallback">
/// A callback function that defines the logic for processing the notification.
/// It accepts an <see cref="INotification"/> and a <see cref="CancellationToken"/> as parameters
/// and returns a <see cref="Task"/> representing the asynchronous operation.
/// </param>
public record NotificationHandlerExecutor(object HandlerInstance, Func<INotification, CancellationToken, Task> HandlerCallback);
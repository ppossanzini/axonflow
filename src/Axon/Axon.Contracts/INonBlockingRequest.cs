using MediatR;

namespace Axon;

/// <summary>
/// Marker interface to represent a request that can be sent using the Publish method
/// </summary>
public interface INonBlockingRequest : MediatR.IRequest, INotification
{
}

/// <summary>
/// Marker interface to represent a request that can be sent using the Publish method
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface INonBlockingRequest<out TResponse> : MediatR.IRequest<TResponse>, INotification
{
    
}
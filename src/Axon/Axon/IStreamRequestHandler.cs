using System.Collections.Generic;
using System.Threading;


namespace Axon;

/// <summary>
/// Defines a handler for a stream request using IAsyncEnumerable as return type.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface IStreamRequestHandler<in TRequest, out TResponse> : MediatR.IStreamRequestHandler<TRequest, TResponse>
  where TRequest : MediatR.IStreamRequest<TResponse>
{
}
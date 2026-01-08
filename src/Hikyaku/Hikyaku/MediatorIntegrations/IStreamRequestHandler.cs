using System.Collections.Generic;
using System.Threading;


namespace MediatR;

/// <summary>
/// Defines a handler for a stream request using IAsyncEnumerable as return type.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface IStreamRequestHandler<in TRequest, out TResponse>:  Hikyaku.IStreamRequestHandler<TRequest, TResponse>
    where TRequest : Hikyaku.IStreamRequest<TResponse>
{

}

using System.Threading;
using System.Threading.Tasks;


namespace Axon;

/// <summary>
/// Defines a handler for a request
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface IRequestHandler<in TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse>
  where TRequest : MediatR.IRequest<TResponse>
{
}

/// <summary>
/// Defines a handler for a request with a void response.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
public interface IRequestHandler<in TRequest> : MediatR.IRequestHandler<TRequest>
  where TRequest : MediatR.IRequest
{
}
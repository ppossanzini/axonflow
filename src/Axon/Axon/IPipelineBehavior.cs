
namespace Axon;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents an async continuation for the next task to execute in the pipeline
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
/// <returns>Awaitable task returning a <typeparamref name="TResponse"/></returns>
public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken t = default);

/// <summary>
/// Pipeline behavior to surround the inner handler.
/// Implementations add additional behavior and await the next delegate.
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public interface IPipelineBehavior<in TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse> 
  where TRequest : notnull
{

}
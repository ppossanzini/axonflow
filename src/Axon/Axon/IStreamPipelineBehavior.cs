namespace Axon;

using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Represents an async enumerable continuation for the next task to execute in the pipeline
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
/// <returns>Async Enumerable returning a <typeparamref name="TResponse"/></returns>
public delegate IAsyncEnumerable<TResponse> StreamHandlerDelegate<out TResponse>();

/// <summary>
/// Stream Pipeline behavior to surround the inner handler.
/// Implementations add additional behavior and await the next delegate.
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public interface IStreamPipelineBehavior<in TRequest, TResponse> : MediatR.IStreamPipelineBehavior<TRequest, TResponse>
  where TRequest : notnull
{
}
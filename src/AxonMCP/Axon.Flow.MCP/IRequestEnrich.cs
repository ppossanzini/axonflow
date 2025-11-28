namespace Axon.Flow.MCP;

public interface IRequestEnrich<in TRequest, TResponse>: MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
}
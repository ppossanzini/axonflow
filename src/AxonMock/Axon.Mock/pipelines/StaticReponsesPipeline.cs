using Microsoft.Extensions.Options;

namespace Axon.Mock.pipelines;

public class StaticReponsesPipeline<TRequest, TResponse>(IOptions<StaticResponseOptions> options) : IPipelineBehavior<TRequest, TResponse>
  where TRequest : class, IBaseRequest
{

  private readonly StaticResponseOptions _options = options.Value;
  
  public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
  }
}
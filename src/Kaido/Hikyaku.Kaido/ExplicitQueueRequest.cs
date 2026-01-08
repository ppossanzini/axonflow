
namespace Hikyaku.Kaido
{
  public class ExplicitQueueRequest<T> : MediatR.IRequest, IExplicitQueue where T : MediatR.IRequest
  {
    public T Message { get; set; }
    public string QueueName { get; set; }

    object IExplicitQueue.MessageObject
    {
      get => Message;
    }
  }

  public class ExplicitQueueRequest<T, TResponse> : MediatR.IRequest<TResponse>, IExplicitQueue where T : MediatR.IRequest<TResponse>
  {
    public T Message { get; set; }
    public string QueueName { get; set; }

    object IExplicitQueue.MessageObject
    {
      get => Message;
    }
  }
}
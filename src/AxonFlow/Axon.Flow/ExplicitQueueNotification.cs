
using System;

namespace Axon.Flow
{
  [Obsolete("Use IRouteTo interface instead", true)]
  public class ExplicitQueueNotification<T> : IExplicitQueue, MediatR.INotification
    where T : MediatR.INotification
  {
    public T Message { get; set; }
    public string QueueName { get; set; }

    object IExplicitQueue.MessageObject
    {
      get => Message;
    }
  }

  interface IExplicitQueue
  {
    string QueueName { get; }
    object MessageObject { get; }
  }
}
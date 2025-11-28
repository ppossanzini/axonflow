using System.Threading;
using System.Threading.Tasks;

namespace Axon.Flow
{
  public interface IExternalMessageDispatcher
  {
    bool CanDispatch<TRequest>();

    Task<Messages.ResponseMessage<TResponse>> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default);

    Task Notify<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : MediatR.INotification;
  }
}
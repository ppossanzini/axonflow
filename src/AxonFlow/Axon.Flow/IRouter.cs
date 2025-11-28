using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axon.Flow
{
  public interface IRouter
  {
    bool HasLocalHandler<T>() where T : MediatR.IBaseRequest;
    bool HasLocalHandler(Type t);
    bool HasRemoteHandler<T>() where T : MediatR.IBaseRequest;
    bool HasRemoteHandler(Type t);
    HandlerLocation GetLocation(Type t);
    HandlerLocation GetLocation<T>();
    Task<TResponse> InvokeRemoteHandler<TRequest, TResponse>(TRequest request); 
    Task SendRemoteNotification<TRequest>(TRequest request) where TRequest : MediatR.INotification;

    IEnumerable<Type> GetLocalRequestsTypes();
    IEnumerable<Type> GetRemoteRequestsTypes();
  }
}
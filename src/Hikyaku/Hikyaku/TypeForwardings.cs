using System.Runtime.CompilerServices;
using Hikyaku;


[assembly: TypeForwardedTo(typeof(MediatR.IBaseRequest))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequest<>))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequest))]
[assembly: TypeForwardedTo(typeof(MediatR.INotification))]
[assembly: TypeForwardedTo(typeof(MediatR.Unit))]
// [assembly: TypeForwardedTo(typeof(MediatR.IMediator))]
using System.Runtime.CompilerServices;
using Axon;


[assembly: TypeForwardedTo(typeof(MediatR.IBaseRequest))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequest<>))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequest))]
[assembly: TypeForwardedTo(typeof(MediatR.INotification))]
[assembly: TypeForwardedTo(typeof(MediatR.Unit))]
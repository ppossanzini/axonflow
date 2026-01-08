using System.Runtime.CompilerServices;
using Hikyaku;


[assembly: TypeForwardedTo(typeof(MediatR.IBaseRequest))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequest<>))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequest))]
[assembly: TypeForwardedTo(typeof(MediatR.INotification))]
[assembly: TypeForwardedTo(typeof(MediatR.Unit))]
[assembly: TypeForwardedTo(typeof(MediatR.IMediator))]
[assembly: TypeForwardedTo(typeof(MediatR.IPipelineBehavior<,>))]
[assembly: TypeForwardedTo(typeof(MediatR.INotificationHandler<>))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequestHandler<>))]
[assembly: TypeForwardedTo(typeof(MediatR.IRequestHandler<,>))]
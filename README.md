![Logo.png](assets/logo-250.png)

[![NuGet](https://img.shields.io/nuget/dt/AxonFlow.svg)](https://www.nuget.org/packages/AxonFlow) 
[![NuGet](https://img.shields.io/nuget/vpre/AxonFlow.svg)](https://www.nuget.org/packages/AxonFlow)


AxonFlow is a open-source derivate work of [MediatR](https://github.com/jbogard/MediatR.Archive) version 12.5.0 
with additions to transform message dispatching from in-process to Out-Of-Process messaging via RPC calls implemented with popular message dispatchers. 



## When you need AxonFlow. 

Axon is very good to implement some patterns like [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

Implementing CQRS in an in-process application does not bring to you all the power of the pattern but gives you the opportunity to have organized, easy to maintain code. When the application grow you may need to refactor your things to a microservices architecture.

Microservices and patterns like CQRS are very powerfull combination. In this scenario you will need to rewrite communication part to use some kind of out of process message dispatcher.

AxonFlow provides you message dispatching behaviour and let you decide which call needs to be in-process and which needs to be Out-of-process and dispatched remotely, via a configuration without changing a single row of your code.

 

## Installation

You should install [AxonFlow with NuGet](https://www.nuget.org/packages/AxonFlow)

    Install-Package Axon

if you need out-of-process functions

    Install-Package Axon-Flow
    
Or via the .NET Core command line interface:

    dotnet add package Axon
    dotnet add package Axon-Flow

Either commands, from Package Manager Console or .NET Core CLI, will download and install AxonFlow and all required dependencies.


## Using Contracts-Only Package
To reference only the contracts for AxonFlow, which includes:

IRequest (including generic variants)
INotification
IStreamRequest
Add a package reference to Axon-Contracts

This package is useful in scenarios where your AxonFlow contracts are in a separate assembly/project from handlers. Example scenarios include:

API contracts
GRPC contracts
Blazor

## Basic Configuration using `IServiceCollection`

Configuring AxonFlow is an easy task. 
1) Add AxonFlow to services configuration via AddAxon extension method. this will register the Axon service that can be used for message dispatching. 

Axon supports `Microsoft.Extensions.DependencyInjection.Abstractions` directly. To register various Axon services and handlers:

```
services.AddAxon(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
```

or with an assembly:

```
services.AddAxon(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
```


This registers:

- `IAxon` as transient
- `ISender` as transient
- `IPublisher` as transient
- `IRequestHandler<,>` concrete implementations as transient
- `IRequestHandler<>` concrete implementations as transient
- `INotificationHandler<>` concrete implementations as transient
- `IStreamRequestHandler<>` concrete implementations as transient
- `IRequestExceptionHandler<,,>` concrete implementations as transient
- `IRequestExceptionAction<,>)` concrete implementations as transient

This also registers open generic implementations for:

- `INotificationHandler<>`
- `IRequestExceptionHandler<,,>`
- `IRequestExceptionAction<,>`

To register behaviors, stream behaviors, pre/post processors:

```csharp
services.AddAxon(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
    cfg.AddBehavior<PingPongBehavior>();
    cfg.AddStreamBehavior<PingPongStreamBehavior>();
    cfg.AddRequestPreProcessor<PingPreProcessor>();
    cfg.AddRequestPostProcessor<PingPongPostProcessor>();
    cfg.AddOpenBehavior(typeof(GenericBehavior<,>));
    });
```


If no configuration is given for the message dispatched, all messages are dispatched in-process.
You can change the default behaviour in using the following configuration

2) Decide what is the default behaviour, available options are 
   1) ***ImplicitLocal*** : all `axon.Send()` calls will be delivered in-process unless further configuration. 
   2) ***ImplicitRemote*** : all `axon.Send()` calls will be delivered out-of-process unless further configuration. 
   3) ***Explicit*** : you have the responsability do declare how to manage every single call. 


```
    services.AddAxonFlow(opt =>
    {
      opt.Behaviour = AxonFlowBehaviourEnum.Explicit;
    });
```


3) Configure calls delivery type according with you behaviour:

```
    services.AddAxonFlow(opt =>
    {
      opt.Behaviour = AxonFlowBehaviourEnum.Explicit;
      opt.SetAsRemoteRequest<Request1>();
      opt.SetAsRemoteRequest<Request2>();
      ....
    }
```

Of course you will have some processes with requests declared **Local** and other processes with same requests declared **Remote**.

### Example of process with all local calls and some remote calls

```
    services.AddAxonFlow(opt =>
    {
      opt.Behaviour = AxonFlowBehaviourEnum.ImplicitLocal;
      opt.SetAsRemoteRequest<Request1>();
      opt.SetAsRemoteRequest<Request2>();
      opt.SetAsRemoteRequests(typeof(Request2).Assembly); // All requests in an assembly
    });
```


### Example of process with local handlers. 

```
    services.AddAxonFlow(opt =>
    {
      opt.Behaviour = AxonFlowBehaviourEnum.ImplicitLocal;
    });

```

### Example of process with remore handlers. 

```
    services.AddAxonFlow(opt =>
    {
      opt.Behaviour = AxonFlowBehaviourEnum.ImplicitRemote;
    });
```


# AxonFlow with RabbitMQ


## Installing AxonFlow RabbitMQ extension.

```
    Install-Package Axon-Flow-RabbitMQ
```
    
Or via the .NET Core command line interface:

```
    dotnet add package Axon-Flow-RabbitMQ
```

## Configuring RabbitMQ Extension. 

Once installed you need to configure rabbitMQ extension. 

```
    services.AddAxonFlowRabbitMQMessageDispatcher(opt =>
    {
      opt.HostName = "rabbit instance";
      opt.Port = 5672;
      opt.Password = "password";
      opt.UserName = "rabbituser";
      opt.VirtualHost = "/";
    });
    services.ResolveAxonFlowCalls();
```

or if you prefer use appsettings configuration 

```
    services.AddAxonFlowRabbitMQMessageDispatcher(opt => context.Configuration.GetSection("rabbitmq").Bind(opt));
    services.ResolveAxonFlowCalls();
```


# AxonFlow with Kafka

## Installing AxonFlow Kafka extension.

```
    Install-Package Axon-Flow-Kafka
```
    
Or via the .NET Core command line interface:

```
    dotnet add package Axon-Flow-Kafka
```


## Configuring Kafka Extension. 

Once installed you need to configure Kafka extension. 

```
    services.AddAxonFlowKafkaMessageDispatcher(opt =>
    {
      opt.BootstrapServers = "localhost:9092";
    });
    services.ResolveAxonFlowCalls();
```

or if you prefer use appsettings configuration 

```
    services.AddAxonFlowKafkaMessageDispatcher(opt => context.Configuration.GetSection("kafka").Bind(opt));
    services.ResolveAxonFlowCalls();
```



# AxonFlow with Azure Message Queues

Coming soon. 

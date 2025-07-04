using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AxonFlow;
using AxonFlow.GRPC;

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Type = System.Type;

namespace AxonFlow.GRPC
{
  public class RequestsManager : global::AxonFlow.GRPC.GrpcServices.GrpcServicesBase
  {
    private readonly ILogger<RequestsManager> _logger;
    private readonly IServiceProvider _provider;
    private readonly MessageDispatcherOptions _options;

    private readonly HashSet<string> _deDuplicationCache = new HashSet<string>();
    private readonly SHA256 _hasher = SHA256.Create();

    private readonly Dictionary<string, Type> _typeMappings;

    public RequestsManager(IOptions<MessageDispatcherOptions> options, ILogger<RequestsManager> logger, IServiceProvider provider,
      IOptions<RouterOptions> routerOptions, IOptions<RequestsManagerOptions> requestsManagerOptions)
    {
      if (requestsManagerOptions.Value.AcceptMessageTypes.Count == 0)
      {
        foreach (var t in routerOptions.Value.LocalTypes)
          requestsManagerOptions.Value.AcceptMessageTypes.Add(t);
      }

      this._options = options.Value;
      this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
      this._provider = provider;

      _typeMappings = requestsManagerOptions.Value.AcceptMessageTypes.ToDictionary(k => k.AxonTypeName(routerOptions.Value), v => v);
    }


    public override async Task<MessageResponse> ManageAxonFlowMessage(RequestMessage request, ServerCallContext context)
    {
      _logger.LogDebug("Message Received. Looking for type");
      if (_typeMappings.TryGetValue(request.AxonFlowType, out var messageType))
      {
        var consumerMethod = typeof(RequestsManager)
          .GetMethod(nameof(RequestsManager.ManageGenericAxonFlowMessage), BindingFlags.Instance | BindingFlags.NonPublic)?
          .MakeGenericMethod(messageType);

        var response = await ((Task<string>)consumerMethod!.Invoke(this, new object[] { request }))!;
        return new MessageResponse() { Body = response };
      }

      return null;
    }

    public override async Task<Empty> ManageAxonFlowNotification(NotifyMessage request, ServerCallContext context)
    {
      _logger.LogDebug("Notification Received. Looking for type");
      if (_typeMappings.TryGetValue(request.AxonFlowType, out var messageType))
      {
        var consumerMethod = typeof(RequestsManager)
          .GetMethod(nameof(RequestsManager.ManageGenericAxonFlowNotification), BindingFlags.Instance | BindingFlags.NonPublic)?
          .MakeGenericMethod(messageType);

        await ((Task)consumerMethod!.Invoke(this, new object[] { request }))!;
      }

      return new Empty();
    }

    private async Task<string> ManageGenericAxonFlowMessage<T>(RequestMessage request)
    {
      string responseMsg = null;
      try
      {
        var msg = request.Body;
        _logger.LogDebug("Elaborating message : {0}", msg);
        var message = JsonConvert.DeserializeObject<T>(msg, _options.SerializerSettings);


        var orchestrator = _provider.CreateScope().ServiceProvider.GetRequiredService<IAxon>();
        var response = await orchestrator.Send(message);
        responseMsg = JsonConvert.SerializeObject(new Messages.ResponseMessage { Content = response, Status = Messages.StatusEnum.Ok },
          _options.SerializerSettings);
        _logger.LogDebug("Elaborating sending response : {0}", responseMsg);
      }
      catch (Exception ex)
      {
        responseMsg = JsonConvert.SerializeObject(new Messages.ResponseMessage
          {
            Exception = ex,
            OriginaStackTrace = ex.StackTrace?.ToString(),
            Status = Messages.StatusEnum.Exception, Content = Unit.Value
          },
          _options.SerializerSettings);
        _logger.LogError(ex, $"Error executing message of type {typeof(T)} from external service");
      }

      return responseMsg;
    }

    private async Task ManageGenericAxonFlowNotification<T>(NotifyMessage request)
    {
      var axon = _provider.CreateScope().ServiceProvider.GetRequiredService<IAxon>();
      var router = axon as AxonFlow;
      try
      {
        var msg = request.Body;

        if (_options.DeDuplicationEnabled)
        {
          var hash = msg.GetHash(_hasher);
          lock (_deDuplicationCache)
            if (_deDuplicationCache.Contains(hash))
            {
              _logger.LogDebug($"duplicated message received : {request.AxonFlowType}");
              return;
            }

          lock (_deDuplicationCache)
            _deDuplicationCache.Add(hash);

          // Do not await this task
#pragma warning disable CS4014
          Task.Run(async () =>
          {
            await Task.Delay(_options.DeDuplicationTTL);
            lock (_deDuplicationCache)
              _deDuplicationCache.Remove(hash);
          });
#pragma warning restore CS4014
        }

        _logger.LogDebug("Elaborating notification : {0}", msg);
        var message = JsonConvert.DeserializeObject<T>(msg, _options.SerializerSettings);

        router?.StopPropagating();
        await axon.Publish(message);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Error executing message of type {typeof(T)} from external service");
      }
      finally
      {
        router?.ResetPropagating();
      }
    }
  }
}
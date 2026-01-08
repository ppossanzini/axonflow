using System;
using System.Collections.Generic;
using Grpc.Net.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hikyaku.Kaido.GRPC
{
  public class MessageDispatcherOptions
  {
    /// <summary>
    /// Gets or sets the time-to-live value for deduplication.
    /// </summary>
    /// <remarks>
    /// The DeDuplicationTTL property determines the amount of time, in milliseconds, that an item can be considered duplicate
    /// before it is removed from the deduplication cache. The default value is 5000 milliseconds (5 seconds).
    /// </remarks>
    /// <value>
    /// The time-to-live value for deduplication.
    /// </value>
    public int DeDuplicationTTL { get; set; } = 5000;

    /// <summary>
    /// Gets or sets a value indicating whether duplicate entries are enabled for deduplication.
    /// </summary>
    /// <value>
    /// <c>true</c> if deduplication is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool DeDuplicationEnabled { get; set; } = true;

    public string DefaultServiceUri { get; set; }

    public GrpcChannelOptions ChannelOptions { get; set; } = new();

    public Dictionary<Type, RemoteServiceDefinition> RemoteTypeServices { get; set; } = new();

    public HashSet<Type> DispatchOnly { get; private set; } = new HashSet<Type>();
    public HashSet<Type> DontDispatch { get; private set; } = new HashSet<Type>();

    public JsonSerializerSettings SerializerSettings { get; set; }

    public MessageDispatcherOptions()
    {
      SerializerSettings = new JsonSerializerSettings
      {
        MissingMemberHandling = MissingMemberHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        DateTimeZoneHandling = DateTimeZoneHandling.Utc
      };
      SerializerSettings.Converters.Add(new StringEnumConverter());
    }
  }
}
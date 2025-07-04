using Grpc.Net.Client;

namespace Axon.Flow.GRPC;

public class RemoteServiceDefinition
{
  public string Uri { get; set; }
  public GrpcChannelOptions ChannelOptions { get; set; }
}
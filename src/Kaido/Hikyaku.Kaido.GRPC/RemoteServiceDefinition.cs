using Grpc.Net.Client;

namespace Hikyaku.Kaido.GRPC;

public class RemoteServiceDefinition
{
  public string Uri { get; set; }
  public GrpcChannelOptions ChannelOptions { get; set; }
}
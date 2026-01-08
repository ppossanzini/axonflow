using System;
using System.Collections.Generic;

namespace Hikyaku.Kaido.GRPC;

public class RequestsManagerOptions
{
  public HashSet<Type> AcceptMessageTypes { get; private set; } = new HashSet<Type>();
}
using System;

namespace Hikyaku.Kaido
{
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueTimeoutAttribute : Attribute
  {
    public int ConsumerTimeout { get; set; }
  }
}
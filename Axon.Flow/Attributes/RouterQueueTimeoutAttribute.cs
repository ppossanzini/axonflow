using System;

namespace Axon.Flow
{
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueTimeoutAttribute : Attribute
  {
    public int ConsumerTimeout { get; set; }
  }
}
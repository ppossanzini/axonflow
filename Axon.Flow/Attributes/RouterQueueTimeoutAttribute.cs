using System;

namespace Axon.Flow
{
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueTimeoutAttribute : System.Attribute
  {
    public int ConsumerTimeout { get; set; }
  }
}
using System;

namespace AxonFlow
{
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueTimeoutAttribute : System.Attribute
  {
    public int ConsumerTimeout { get; set; }
  }
}
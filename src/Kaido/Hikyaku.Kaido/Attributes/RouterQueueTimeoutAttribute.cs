using System;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CheckNamespace
namespace Hikyaku.Kaido
{
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueTimeoutAttribute : Attribute
  {
    public int ConsumerTimeout { get; set; }
  }
}
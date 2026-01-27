using System;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CheckNamespace
namespace Hikyaku.Kaido
{
  /// <summary>
  /// Represents an attribute used to specify the name of the Hikyaku.Router queue for a class.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueNameAttribute : Attribute
  {
    public string Name { get; set; }
    public bool Absolute { get; set; }
  }
}
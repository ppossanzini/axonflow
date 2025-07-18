using System;

namespace Axon.Flow
{
  /// <summary>
  /// Represents an attribute used to specify the name of the Axon.Router queue for a class.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueNameAttribute : Attribute
  {
    public string Name { get; set; }
    public bool Absolute { get; set; }
  }
}
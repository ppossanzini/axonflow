using System;

namespace AxonFlow
{
  /// <summary>
  /// Represents an attribute used to specify the name of the AxonFlow.Router queue for a class.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class RouterQueueNameAttribute : System.Attribute
  {
    public string Name { get; set; }
  }
}
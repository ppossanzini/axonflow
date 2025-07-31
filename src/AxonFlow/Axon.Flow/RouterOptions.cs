using System;
using System.Collections.Generic;

namespace Axon.Flow
{
  public class RouterOptions
  {
    public string DefaultQueuePrefix { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the behavior of the axonflow.
    /// </summary>
    /// <value>
    /// The behaviour of the axon flow.
    /// </value>
    public AxonFlowBehaviourEnum Behaviour { get; set; } = AxonFlowBehaviourEnum.ImplicitLocal;

    /// <summary>
    /// Gets or sets the collection of local requests.
    /// </summary>
    /// <value>The local requests.</value>
    public HashSet<Type> LocalTypes { get; private set; } = new HashSet<Type>();

    /// <summary>
    /// Gets the set of remote requests supported by the application.
    /// </summary>
    public HashSet<Type> RemoteTypes { get; private set; } = new HashSet<Type>();

    /// <summary>
    /// Get the prefix of remote queue
    /// </summary>
    public Dictionary<string, string> TypePrefixes { get; private set; } = new Dictionary<string, string>();

    public Dictionary<Type, HashSet<string>> QueueNames { get; private set; } = new Dictionary<Type, HashSet<string>>();
  }


  /// <summary>
  /// Specifies the possible behaviours of an arbitrator.
  /// </summary>
  public enum AxonFlowBehaviourEnum
  {
    ImplicitLocal,
    ImplicitRemote,
    Explicit
  }
}
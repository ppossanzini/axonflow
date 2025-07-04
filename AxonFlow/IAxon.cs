namespace AxonFlow;

/// <summary>
/// Defines a orchestrator to encapsulate request/response and publishing interaction patterns
/// </summary>
public interface IAxon : ISender, IPublisher
{
}
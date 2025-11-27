using MediatR;

namespace Axon;

/// <summary>
/// Defines a orchestrator to encapsulate request/response and publishing interaction patterns
/// </summary>
public interface IAxon : IAxonSender, IAxonPublisher
{
}
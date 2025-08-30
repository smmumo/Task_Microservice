

namespace Order.Application.Messaging;

/// <summary>
/// Represents the integration event publisher interface.
/// </summary>
public interface IIntegrationEventPublisher
{
    /// <summary>
    /// Publishes the specified integration event to the message queue.
    /// </summary> 
    /// <returns>The completed task.</returns>
    void Publish(object integrationEvent);
}


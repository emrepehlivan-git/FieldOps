namespace FieldOps.BuildingBlocks.Messaging;

public interface IIntegrationMessagePublisher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, IIntegrationCommand;

    Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent;
}

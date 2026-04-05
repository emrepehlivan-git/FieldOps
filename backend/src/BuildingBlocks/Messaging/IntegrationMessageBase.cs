namespace FieldOps.BuildingBlocks.Messaging;

public abstract record IntegrationMessageBase : IIntegrationMessage
{
    public Guid MessageId { get; init; } = Guid.CreateVersion7();

    public DateTimeOffset OccurredAt { get; init; } = DateTimeOffset.UtcNow;

    public MessageMetadata Metadata { get; init; } = new();
}

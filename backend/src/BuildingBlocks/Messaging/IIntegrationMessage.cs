namespace FieldOps.BuildingBlocks.Messaging;

public interface IIntegrationMessage
{
    Guid MessageId { get; init; }

    DateTimeOffset OccurredAt { get; init; }

    MessageMetadata Metadata { get; init; }
}

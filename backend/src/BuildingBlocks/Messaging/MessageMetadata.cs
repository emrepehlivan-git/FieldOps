namespace FieldOps.BuildingBlocks.Messaging;

public record MessageMetadata
{
    public Guid? CorrelationId { get; init; }

    public Guid? CausationId { get; init; }
}

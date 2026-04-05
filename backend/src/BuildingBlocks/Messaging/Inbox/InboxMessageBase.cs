using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Persistence.Entities;

namespace FieldOps.BuildingBlocks.Messaging.Inbox;

public abstract class InboxMessageBase : EntityBase<Guid>
{
    protected InboxMessageBase()
    {
    }

    protected InboxMessageBase(
        Guid id,
        Guid integrationMessageId,
        string messageType,
        DateTimeOffset receivedAtUtc)
        : base(id)
    {
        Guard.ThrowIfDefault(id);
        Guard.ThrowIfDefault(integrationMessageId);
        Guard.ThrowIfNullOrWhiteSpace(messageType);

        IntegrationMessageId = integrationMessageId;
        MessageType = messageType;
        ReceivedAtUtc = receivedAtUtc;
        Status = InboxMessageStatus.Received;
    }

    public Guid IntegrationMessageId { get; protected set; }

    public string MessageType { get; protected set; } = default!;

    public DateTimeOffset ReceivedAtUtc { get; protected set; }

    public InboxMessageStatus Status { get; protected set; }

    public DateTimeOffset? ProcessedAtUtc { get; protected set; }

    public string? LastError { get; protected set; }

    public void MarkProcessed(DateTimeOffset atUtc)
    {
        Status = InboxMessageStatus.Processed;
        ProcessedAtUtc = atUtc;
        LastError = null;
    }

    public void MarkFailed(string error, DateTimeOffset atUtc)
    {
        Guard.ThrowIfNullOrWhiteSpace(error);

        Status = InboxMessageStatus.Failed;
        LastError = error;
        ProcessedAtUtc = atUtc;
    }
}

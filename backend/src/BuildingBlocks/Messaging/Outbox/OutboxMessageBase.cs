using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Persistence.Entities;

namespace FieldOps.BuildingBlocks.Messaging.Outbox;

public abstract class OutboxMessageBase : EntityBase<Guid>
{
    protected OutboxMessageBase()
    {
    }

    protected OutboxMessageBase(Guid id, string messageType, string payload, DateTimeOffset createdAtUtc)
        : base(id)
    {
        Guard.ThrowIfDefault(id);
        Guard.ThrowIfNullOrWhiteSpace(messageType);
        Guard.ThrowIfNull(payload);

        MessageType = messageType;
        Payload = payload;
        CreatedAtUtc = createdAtUtc;
        Status = OutboxMessageStatus.Pending;
    }

    public string MessageType { get; protected set; } = default!;

    public string Payload { get; protected set; } = default!;

    public DateTimeOffset CreatedAtUtc { get; protected set; }

    public OutboxMessageStatus Status { get; protected set; }

    public DateTimeOffset? PublishedAtUtc { get; protected set; }

    public string? LastError { get; protected set; }

    public int AttemptCount { get; protected set; }

    public void MarkPublished(DateTimeOffset atUtc)
    {
        Status = OutboxMessageStatus.Published;
        PublishedAtUtc = atUtc;
        LastError = null;
    }

    public void RecordSendFailure(string error)
    {
        Guard.ThrowIfNullOrWhiteSpace(error);

        AttemptCount++;
        LastError = error;
    }

    public void MarkAbandoned(string reason)
    {
        Guard.ThrowIfNullOrWhiteSpace(reason);

        Status = OutboxMessageStatus.Failed;
        LastError = reason;
    }
}

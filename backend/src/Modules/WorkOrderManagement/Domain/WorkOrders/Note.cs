using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Persistence.Entities;

namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public sealed class Note : EntityBase<Guid>
{
    public string Text { get; private set; } = string.Empty;
    public Guid CreatedByUserId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private Note()
    {
    }

    private Note(Guid id, string text, Guid createdByUserId, DateTimeOffset createdAt)
        : base(id)
    {
        Guard.ThrowIfNullOrWhiteSpace(text);
        Guard.ThrowIfEmpty(createdByUserId);

        Text = text.Trim();
        CreatedByUserId = createdByUserId;
        CreatedAt = createdAt;
    }

    public static Note Create(Guid id, string text, Guid createdByUserId, DateTimeOffset createdAt) =>
        new(id, text, createdByUserId, createdAt);
}


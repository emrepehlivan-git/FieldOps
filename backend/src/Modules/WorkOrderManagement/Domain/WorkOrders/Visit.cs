using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Persistence.Entities;

namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public sealed class Visit : EntityBase<Guid>
{
    public Guid TechnicianId { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }
    public DateTimeOffset? EndedAt { get; private set; }
    public string? Summary { get; private set; }

    private Visit()
    {
    }

    private Visit(Guid id, Guid technicianId, DateTimeOffset startedAt)
        : base(id)
    {
        Guard.ThrowIfEmpty(technicianId);

        TechnicianId = technicianId;
        StartedAt = startedAt;
    }

    public static Visit Start(Guid id, Guid technicianId, DateTimeOffset startedAt) =>
        new(id, technicianId, startedAt);

    public void End(DateTimeOffset endedAt, string? summary)
    {
        if (EndedAt is not null)
            throw new InvalidOperationException(WorkOrderValidationMessages.VisitAlreadyEnded);
        Guard.ThrowIfFalse(
            endedAt,
            endedAt >= StartedAt,
            WorkOrderValidationMessages.VisitEndedAtCannotBeBeforeStartedAt);

        EndedAt = endedAt;
        Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
    }
}


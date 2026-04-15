using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Exceptions;
using FieldOps.BuildingBlocks.Persistence.Entities;
using FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders.Events;

namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public sealed class WorkOrder : AggregateRootBase<Guid>
{
    private readonly List<Visit> _visits = [];
    private readonly List<Note> _notes = [];
    private readonly List<Attachment> _attachments = [];
    private readonly List<object> _domainEvents = [];

    public Guid ServiceRequestId { get; private set; }
    public Guid? SlaId { get; private set; }

    public WorkOrderStatus Status { get; private set; }
    public WorkOrderPriority Priority { get; private set; }

    public Location? Location { get; private set; }
    public TimeWindow? ServiceWindow { get; private set; }

    public Guid? AssignedTechnicianId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public DateTimeOffset? ClosedAt { get; private set; }

    public IReadOnlyCollection<Visit> Visits => _visits;
    public IReadOnlyCollection<Note> Notes => _notes;
    public IReadOnlyCollection<Attachment> Attachments => _attachments;

    public IReadOnlyCollection<object> DomainEvents => _domainEvents;

    private WorkOrder()
    {
    }

    private WorkOrder(
        Guid id,
        Guid serviceRequestId,
        WorkOrderPriority priority,
        Location? location,
        TimeWindow? serviceWindow,
        Guid? slaId,
        DateTimeOffset createdAt)
        : base(id)
    {
        Guard.ThrowIfEmpty(serviceRequestId);

        ServiceRequestId = serviceRequestId;
        Priority = priority;
        Location = location;
        ServiceWindow = serviceWindow;
        SlaId = slaId;
        CreatedAt = createdAt;
        Status = WorkOrderStatus.Created;

        Raise(new WorkOrderCreatedDomainEvent(id, serviceRequestId));
    }

    public static WorkOrder Create(
        Guid id,
        Guid serviceRequestId,
        WorkOrderPriority priority,
        Location? location,
        TimeWindow? serviceWindow,
        Guid? slaId,
        DateTimeOffset createdAt) =>
        new(id, serviceRequestId, priority, location, serviceWindow, slaId, createdAt);

    public void AssignTechnician(Guid technicianId)
    {
        Guard.ThrowIfEmpty(technicianId);

        if (Status is WorkOrderStatus.Completed or WorkOrderStatus.Closed)
            throw new DomainException(WorkOrderDomainErrors.CannotAssignTechnicianWhenCompletedOrClosed);

        AssignedTechnicianId = technicianId;

        if (Status == WorkOrderStatus.Created)
            Status = WorkOrderStatus.Assigned;

        Raise(new TechnicianAssignedDomainEvent(Id, technicianId));
    }

    public Visit StartWork(Guid visitId, Guid technicianId, DateTimeOffset startedAt)
    {
        Guard.ThrowIfEmpty(visitId);
        Guard.ThrowIfEmpty(technicianId);

        if (AssignedTechnicianId is null)
            throw new DomainException(WorkOrderDomainErrors.CannotStartWorkBeforeTechnicianAssigned);

        if (AssignedTechnicianId != technicianId)
            throw new DomainException(WorkOrderDomainErrors.OnlyAssignedTechnicianCanStartWork);

        if (Status is WorkOrderStatus.Completed or WorkOrderStatus.Closed)
            throw new DomainException(WorkOrderDomainErrors.CannotStartWorkWhenCompletedOrClosed);

        if (Status == WorkOrderStatus.Created)
            Status = WorkOrderStatus.Assigned;

        Status = WorkOrderStatus.InProgress;
        StartedAt ??= startedAt;

        var visit = Visit.Start(visitId, technicianId, startedAt);
        _visits.Add(visit);

        Raise(new WorkStartedDomainEvent(Id, technicianId, startedAt));

        return visit;
    }

    public void CompleteWork(Guid technicianId, DateTimeOffset completedAt, string? visitSummary = null)
    {
        Guard.ThrowIfEmpty(technicianId);

        if (AssignedTechnicianId is null)
            throw new DomainException(WorkOrderDomainErrors.CannotCompleteWorkBeforeTechnicianAssigned);

        if (AssignedTechnicianId != technicianId)
            throw new DomainException(WorkOrderDomainErrors.OnlyAssignedTechnicianCanCompleteWork);

        if (Status != WorkOrderStatus.InProgress)
            throw new DomainException(WorkOrderDomainErrors.WorkOrderMustBeInProgressToBeCompleted);

        var activeVisit = _visits.LastOrDefault(v => v.TechnicianId == technicianId && v.EndedAt is null);
        if (activeVisit is not null)
            activeVisit.End(completedAt, visitSummary);

        Status = WorkOrderStatus.Completed;
        CompletedAt = completedAt;

        Raise(new WorkCompletedDomainEvent(Id, technicianId, completedAt));
    }

    public void Close(DateTimeOffset closedAt)
    {
        if (Status != WorkOrderStatus.Completed)
            throw new DomainException(WorkOrderDomainErrors.WorkOrderMustBeCompletedBeforeClosing);

        Status = WorkOrderStatus.Closed;
        ClosedAt = closedAt;
    }

    public void AddNote(Guid noteId, string text, Guid createdByUserId, DateTimeOffset createdAt)
    {
        if (Status == WorkOrderStatus.Closed)
            throw new DomainException(WorkOrderDomainErrors.CannotAddNotesToClosedWorkOrder);

        _notes.Add(Note.Create(noteId, text, createdByUserId, createdAt));
    }

    public void AddAttachment(
        Guid attachmentId,
        string fileName,
        string contentType,
        long sizeBytes,
        string storageKey,
        Guid uploadedByUserId,
        DateTimeOffset uploadedAt)
    {
        if (Status == WorkOrderStatus.Closed)
            throw new DomainException(WorkOrderDomainErrors.CannotAddAttachmentsToClosedWorkOrder);

        _attachments.Add(
            Attachment.Create(
                attachmentId,
                fileName,
                contentType,
                sizeBytes,
                storageKey,
                uploadedByUserId,
                uploadedAt));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void Raise(object @event) => _domainEvents.Add(@event);
}


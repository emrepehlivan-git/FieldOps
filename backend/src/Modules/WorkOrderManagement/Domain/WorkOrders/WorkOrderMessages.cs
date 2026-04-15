namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public static class WorkOrderDomainErrors
{
    public const string CannotAssignTechnicianWhenCompletedOrClosed =
        "Cannot assign technician for a completed or closed work order.";

    public const string CannotStartWorkBeforeTechnicianAssigned =
        "Cannot start work before a technician is assigned.";

    public const string OnlyAssignedTechnicianCanStartWork =
        "Only the assigned technician can start work.";

    public const string CannotStartWorkWhenCompletedOrClosed =
        "Cannot start work for a completed or closed work order.";

    public const string CannotCompleteWorkBeforeTechnicianAssigned =
        "Cannot complete work before a technician is assigned.";

    public const string OnlyAssignedTechnicianCanCompleteWork =
        "Only the assigned technician can complete work.";

    public const string WorkOrderMustBeInProgressToBeCompleted =
        "Work order must be in progress to be completed.";

    public const string WorkOrderMustBeCompletedBeforeClosing =
        "Work order must be completed before it can be closed.";

    public const string CannotAddNotesToClosedWorkOrder =
        "Cannot add notes to a closed work order.";

    public const string CannotAddAttachmentsToClosedWorkOrder =
        "Cannot add attachments to a closed work order.";
}

public static class WorkOrderValidationMessages
{
    public const string VisitAlreadyEnded = "Visit is already ended.";
    public const string VisitEndedAtCannotBeBeforeStartedAt = "EndedAt cannot be before StartedAt.";
    public const string TimeWindowStartMustBeBeforeEnd = "Start must be before End.";
}


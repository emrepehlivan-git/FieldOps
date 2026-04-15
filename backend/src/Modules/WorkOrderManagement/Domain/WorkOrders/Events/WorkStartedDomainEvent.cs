namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders.Events;

public sealed record WorkStartedDomainEvent(Guid WorkOrderId, Guid TechnicianId, DateTimeOffset StartedAt);


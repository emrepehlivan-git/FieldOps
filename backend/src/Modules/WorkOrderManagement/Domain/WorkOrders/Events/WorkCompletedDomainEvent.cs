namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders.Events;

public sealed record WorkCompletedDomainEvent(Guid WorkOrderId, Guid TechnicianId, DateTimeOffset CompletedAt);


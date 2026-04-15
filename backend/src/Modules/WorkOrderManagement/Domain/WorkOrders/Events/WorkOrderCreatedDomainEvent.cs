namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders.Events;

public sealed record WorkOrderCreatedDomainEvent(Guid WorkOrderId, Guid ServiceRequestId);


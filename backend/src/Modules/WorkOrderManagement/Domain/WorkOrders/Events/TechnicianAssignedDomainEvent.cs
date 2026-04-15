namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders.Events;

public sealed record TechnicianAssignedDomainEvent(Guid WorkOrderId, Guid TechnicianId);


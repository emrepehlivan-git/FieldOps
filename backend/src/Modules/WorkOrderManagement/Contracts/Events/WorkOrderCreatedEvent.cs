using FieldOps.BuildingBlocks.Messaging;

namespace FieldOps.Modules.WorkOrderManagement.Contracts.Events;

public sealed record WorkOrderCreatedEvent(Guid WorkOrderId, Guid ServiceRequestId) : IntegrationEventBase;


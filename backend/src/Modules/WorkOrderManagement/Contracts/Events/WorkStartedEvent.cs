using FieldOps.BuildingBlocks.Messaging;

namespace FieldOps.Modules.WorkOrderManagement.Contracts.Events;

public sealed record WorkStartedEvent(Guid WorkOrderId, Guid TechnicianId, DateTimeOffset StartedAt) : IntegrationEventBase;


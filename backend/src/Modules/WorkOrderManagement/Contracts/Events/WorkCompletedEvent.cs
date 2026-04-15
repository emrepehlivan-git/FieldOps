using FieldOps.BuildingBlocks.Messaging;

namespace FieldOps.Modules.WorkOrderManagement.Contracts.Events;

public sealed record WorkCompletedEvent(Guid WorkOrderId, Guid TechnicianId, DateTimeOffset CompletedAt) : IntegrationEventBase;


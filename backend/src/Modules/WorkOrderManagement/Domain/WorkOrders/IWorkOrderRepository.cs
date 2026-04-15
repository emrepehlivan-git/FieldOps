using FieldOps.BuildingBlocks.Persistence.Repositories;

namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public interface IWorkOrderRepository : IRepository<WorkOrder, Guid>;


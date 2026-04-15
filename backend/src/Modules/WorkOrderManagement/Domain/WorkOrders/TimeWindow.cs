using FieldOps.BuildingBlocks.Guards;

namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public sealed record TimeWindow(DateTimeOffset Start, DateTimeOffset End)
{
    public static TimeWindow Create(DateTimeOffset start, DateTimeOffset end)
    {
        Guard.ThrowIfGreaterOrEqual(start, end, WorkOrderValidationMessages.TimeWindowStartMustBeBeforeEnd);

        return new TimeWindow(start, end);
    }
}


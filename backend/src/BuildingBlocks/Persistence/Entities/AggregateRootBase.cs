namespace FieldOps.BuildingBlocks.Persistence.Entities;

public abstract class AggregateRootBase<TId> : EntityBase<TId>, IAggregateRoot
    where TId : notnull
{
    protected AggregateRootBase()
    {
    }

    protected AggregateRootBase(TId id)
        : base(id)
    {
    }
}

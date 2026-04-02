namespace FieldOps.BuildingBlocks.Persistence.Entities;

public interface IEntity<TId>
    where TId : notnull
{
    TId Id { get; }
}

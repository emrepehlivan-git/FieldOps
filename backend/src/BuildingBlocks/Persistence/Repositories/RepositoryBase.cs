using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Persistence.Entities;

namespace FieldOps.BuildingBlocks.Persistence.Repositories;

public abstract class RepositoryBase<TEntity, TId> : ReadOnlyRepositoryBase<TEntity, TId>, IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public abstract Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        entities.ThrowIfNull();
        foreach (var entity in entities)
            await AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public abstract void Update(TEntity entity);

    public abstract void Remove(TEntity entity);

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        entities.ThrowIfNull();
        foreach (var entity in entities)
            Remove(entity);
    }
}

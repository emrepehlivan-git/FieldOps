using FieldOps.BuildingBlocks.Persistence.Entities;
using FieldOps.BuildingBlocks.Results;

namespace FieldOps.BuildingBlocks.Persistence.Repositories;

public abstract class ReadOnlyRepositoryBase<TEntity, TId> : IReadOnlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public abstract Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    public abstract Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);

    public abstract Task<long> CountAsync(CancellationToken cancellationToken = default);

    public abstract Task<PaginatedResult<TEntity>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}

using FieldOps.BuildingBlocks.Persistence.Entities;
using FieldOps.BuildingBlocks.Results;

namespace FieldOps.BuildingBlocks.Persistence.Repositories;

public interface IReadOnlyRepository<TEntity, in TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);

    Task<long> CountAsync(CancellationToken cancellationToken = default);

    Task<PaginatedResult<TEntity>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}

using FieldOps.BuildingBlocks.Guards;

namespace FieldOps.BuildingBlocks.Results;

public sealed record PaginatedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public long TotalCount { get; init; }

    public PaginatedResult(IReadOnlyList<T> items, int pageNumber, int pageSize, long totalCount)
    {
        Guard.ThrowIfLessThan(pageNumber, 1);
        Guard.ThrowIfLessThan(pageSize, 1);
        Guard.ThrowIfNegative(totalCount);

        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static implicit operator Result<PaginatedResult<T>>(PaginatedResult<T> page) =>
        Result<PaginatedResult<T>>.Success(page);

    public static explicit operator PaginatedResult<T>(Result<PaginatedResult<T>> result) =>
        result.Value;
}

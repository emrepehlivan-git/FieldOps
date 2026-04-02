using System.Collections.Immutable;
using FieldOps.BuildingBlocks.Guards;

namespace FieldOps.BuildingBlocks.Results;

public sealed record Result(bool IsSuccess, ImmutableArray<Error> Errors)
{
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, default);

    public static Result Failure(Error error) =>
        new(false, [error]);

    public static Result Failure(IReadOnlyList<Error> errors)
    {
        Guard.ThrowIfNull(errors);
        Guard.ThrowIfCountNotInRange(errors, 1, int.MaxValue);

        return new(false, [.. errors]);
    }

    public static implicit operator Result(Error error) => Failure(error);

    public static explicit operator bool(Result result) => result.IsSuccess;
}

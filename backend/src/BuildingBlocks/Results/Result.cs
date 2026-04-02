using System.Collections.Immutable;

namespace FieldOps.BuildingBlocks.Results;

public sealed record Result(bool IsSuccess, ImmutableArray<Error> Errors)
{
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, default);

    public static Result Failure(Error error) =>
        new(false, [error]);

    public static Result Failure(IReadOnlyList<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        if (errors.Count == 0)
            throw new ArgumentException("At least one error is required.", nameof(errors));

        return new(false, ImmutableArray.Create(errors.ToArray()));
    }

    public static implicit operator Result(Error error) => Failure(error);

    public static explicit operator bool(Result result) => result.IsSuccess;
}

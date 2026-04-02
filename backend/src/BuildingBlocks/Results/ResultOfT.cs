using System.Collections.Immutable;

namespace FieldOps.BuildingBlocks.Results;

public sealed record Result<T>(bool IsSuccess, T? SuccessValue, ImmutableArray<Error> Errors)
{
    public bool IsFailure => !IsSuccess;

    public T Value =>
        IsSuccess
            ? SuccessValue!
            : throw new InvalidOperationException("Cannot read value from failure result.");

    public T? ValueOrDefault => IsSuccess ? SuccessValue : default;

    public static Result<T> Success(T value) => new(true, value, default);

    public static Result<T> Failure(Error error) =>
        new(false, default, ImmutableArray.Create(error));

    public static Result<T> Failure(IReadOnlyList<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        if (errors.Count == 0)
            throw new ArgumentException("At least one error is required.", nameof(errors));

        return new(false, default, ImmutableArray.Create(errors.ToArray()));
    }

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);

    public static explicit operator T(Result<T> result) => result.Value;

    public static implicit operator Result(Result<T> result) =>
        result.IsSuccess ? Result.Success() : Result.Failure(result.Errors);
}

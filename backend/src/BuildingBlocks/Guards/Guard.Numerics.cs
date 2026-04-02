using System.Numerics;
using System.Runtime.CompilerServices;

namespace FieldOps.BuildingBlocks.Guards;

public static partial class Guard
{
    public static T ThrowIfNegative<T>(
        T value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumberBase<T>
    {
        if (T.IsNegative(value))
            throw new ArgumentOutOfRangeException(parameterName, value, "Value cannot be negative.");

        return value;
    }

    public static T ThrowIfNotPositive<T>(
        T value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumberBase<T>
    {
        if (T.IsNegative(value) || T.IsZero(value))
            throw new ArgumentOutOfRangeException(parameterName, value, "Value must be positive.");

        return value;
    }

    public static T ThrowIfZero<T>(
        T value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumberBase<T>
    {
        if (T.IsZero(value))
            throw new ArgumentOutOfRangeException(parameterName, value, "Value cannot be zero.");

        return value;
    }

    public static T ThrowIfLessThan<T>(
        T value,
        T minInclusive,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(minInclusive) < 0)
            throw new ArgumentOutOfRangeException(parameterName, value, $"Value must be at least {minInclusive}.");

        return value;
    }

    public static T ThrowIfLessOrEqual<T>(
        T value,
        T minExclusive,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(minExclusive) <= 0)
            throw new ArgumentOutOfRangeException(parameterName, value, $"Value must be greater than {minExclusive}.");

        return value;
    }

    public static T ThrowIfGreaterThan<T>(
        T value,
        T maxInclusive,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(maxInclusive) > 0)
            throw new ArgumentOutOfRangeException(parameterName, value, $"Value must not exceed {maxInclusive}.");

        return value;
    }

    public static T ThrowIfGreaterOrEqual<T>(
        T value,
        T maxExclusive,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(maxExclusive) >= 0)
            throw new ArgumentOutOfRangeException(parameterName, value, $"Value must be less than {maxExclusive}.");

        return value;
    }

    public static T ThrowIfNotInRange<T>(
        T value,
        T minInclusive,
        T maxInclusive,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(minInclusive) < 0 || value.CompareTo(maxInclusive) > 0)
            throw new ArgumentOutOfRangeException(parameterName, value, $"Value must be between {minInclusive} and {maxInclusive} inclusive.");

        return value;
    }

    public static int ThrowIfNotPowerOfTwo(
        int value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value <= 0 || (value & (value - 1)) != 0)
            throw new ArgumentOutOfRangeException(parameterName, value, "Value must be a positive power of two.");

        return value;
    }
}

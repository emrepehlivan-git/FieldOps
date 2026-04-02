using System.Runtime.CompilerServices;

namespace FieldOps.BuildingBlocks.Guards;

public static partial class GuardAgainstExtensions
{
    public static TimeSpan ThrowIfNegative(
        this TimeSpan value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(parameterName, value, "TimeSpan cannot be negative.");

        return value;
    }

    public static TimeSpan ThrowIfZero(
        this TimeSpan value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value == TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(parameterName, value, "TimeSpan cannot be zero.");

        return value;
    }

    public static TimeSpan ThrowIfNotInRange(
        this TimeSpan value,
        TimeSpan minInclusive,
        TimeSpan maxInclusive,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < minInclusive || value > maxInclusive)
            throw new ArgumentOutOfRangeException(parameterName, value, $"TimeSpan must be between {minInclusive} and {maxInclusive} inclusive.");

        return value;
    }

    public static ReadOnlyMemory<T> ThrowIfEmpty<T>(
        this ReadOnlyMemory<T> memory,
        [CallerArgumentExpression(nameof(memory))] string? parameterName = null)
    {
        if (memory.IsEmpty)
            throw new ArgumentException("Memory segment cannot be empty.", parameterName);

        return memory;
    }

    public static Memory<T> ThrowIfEmpty<T>(
        this Memory<T> memory,
        [CallerArgumentExpression(nameof(memory))] string? parameterName = null)
    {
        if (memory.IsEmpty)
            throw new ArgumentException("Memory segment cannot be empty.", parameterName);

        return memory;
    }

    public static ReadOnlySpan<T> ThrowIfEmpty<T>(
        this ReadOnlySpan<T> span,
        [CallerArgumentExpression(nameof(span))] string? parameterName = null)
    {
        if (span.IsEmpty)
            throw new ArgumentException("Span cannot be empty.", parameterName);

        return span;
    }

    public static Span<T> ThrowIfEmpty<T>(
        this Span<T> span,
        [CallerArgumentExpression(nameof(span))] string? parameterName = null)
    {
        if (span.IsEmpty)
            throw new ArgumentException("Span cannot be empty.", parameterName);

        return span;
    }

    public static int ThrowIfIndexOutOfRange(
        this int index,
        int count,
        [CallerArgumentExpression(nameof(index))] string? parameterName = null)
    {
        if ((uint)index >= (uint)count)
            throw new ArgumentOutOfRangeException(parameterName, index, $"Index must be in range [0, {count}).");

        return index;
    }

    public static T ThrowIfEqual<T>(
        this T value,
        T forbidden,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        if (comparer.Equals(value, forbidden))
            throw new ArgumentException($"Value must not equal {forbidden}.", parameterName);

        return value;
    }

    public static T ThrowIfNotEqual<T>(
        this T value,
        T expected,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null,
        IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        if (!comparer.Equals(value, expected))
            throw new ArgumentException($"Value must equal {expected}.", parameterName);

        return value;
    }
}

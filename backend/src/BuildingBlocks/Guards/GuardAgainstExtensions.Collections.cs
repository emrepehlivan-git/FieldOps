using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FieldOps.BuildingBlocks.Guards;

public static partial class GuardAgainstExtensions
{
    public static T[] ThrowIfNullOrEmpty<T>(
        [NotNull] this T[]? array,
        [CallerArgumentExpression(nameof(array))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(array, parameterName);
        if (array.Length == 0)
            throw new ArgumentException("Array must contain at least one element.", parameterName);

        return array;
    }

    public static IReadOnlyList<T> ThrowIfNullOrEmpty<T>(
        [NotNull] this IReadOnlyList<T>? list,
        [CallerArgumentExpression(nameof(list))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(list, parameterName);
        if (list.Count == 0)
            throw new ArgumentException("Collection must contain at least one element.", parameterName);

        return list;
    }

    public static ICollection<T> ThrowIfNullOrEmpty<T>(
        [NotNull] this ICollection<T>? collection,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(collection, parameterName);
        if (collection.Count == 0)
            throw new ArgumentException("Collection must contain at least one element.", parameterName);

        return collection;
    }

    public static IEnumerable<T> ThrowIfNullOrEmpty<T>(
        [NotNull] this IEnumerable<T>? source,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(source, parameterName);

        if (source is ICollection<T> genericCollection && genericCollection.Count == 0)
            throw new ArgumentException("Collection must contain at least one element.", parameterName);

        if (source is ICollection nonGeneric && nonGeneric.Count == 0)
            throw new ArgumentException("Collection must contain at least one element.", parameterName);

        if (source.TryGetNonEnumeratedCount(out var count) && count == 0)
            throw new ArgumentException("Collection must contain at least one element.", parameterName);

        using var e = source.GetEnumerator();
        if (!e.MoveNext())
            throw new ArgumentException("Collection must contain at least one element.", parameterName);

        return source;
    }

    public static IReadOnlyList<T> ThrowIfCountNotInRange<T>(
        this IReadOnlyList<T> list,
        int minCount,
        int maxCount,
        [CallerArgumentExpression(nameof(list))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(list, parameterName);
        if (minCount > maxCount)
            throw new ArgumentException("minCount cannot be greater than maxCount.", nameof(minCount));

        var c = list.Count;
        if (c < minCount || c > maxCount)
            throw new ArgumentOutOfRangeException(parameterName, c, $"Collection count must be between {minCount} and {maxCount} inclusive.");

        return list;
    }

    public static ICollection<T> ThrowIfCountNotInRange<T>(
        this ICollection<T> collection,
        int minCount,
        int maxCount,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(collection, parameterName);
        if (minCount > maxCount)
            throw new ArgumentException("minCount cannot be greater than maxCount.", nameof(minCount));

        var c = collection.Count;
        if (c < minCount || c > maxCount)
            throw new ArgumentOutOfRangeException(parameterName, c, $"Collection count must be between {minCount} and {maxCount} inclusive.");

        return collection;
    }

    public static ImmutableArray<T> ThrowIfNullOrEmpty<T>(
        this ImmutableArray<T> array,
        [CallerArgumentExpression(nameof(array))] string? parameterName = null)
    {
        if (array.IsDefault)
            throw new ArgumentNullException(parameterName, "ImmutableArray must be initialized.");

        if (array.IsEmpty)
            throw new ArgumentException("ImmutableArray must contain at least one element.", parameterName);

        return array;
    }
}

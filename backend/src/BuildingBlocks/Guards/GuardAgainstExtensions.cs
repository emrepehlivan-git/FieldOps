using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FieldOps.BuildingBlocks.Guards;

public static partial class GuardAgainstExtensions
{
    public static T ThrowIfNull<T>(
        [NotNull] this T? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : class =>
        value ?? throw new ArgumentNullException(parameterName, "Value cannot be null.");

    public static T ThrowIfNull<T>(
        [NotNull] this T? value,
        string message,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : class =>
        value ?? throw new ArgumentNullException(parameterName, message);

    public static T ThrowIfNull<T>(
        [NotNull] this T? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : struct =>
        value ?? throw new ArgumentNullException(parameterName, "Nullable value must have a value.");

    public static T ThrowIfDefault<T>(
        this T value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new ArgumentOutOfRangeException(parameterName, value, "Value cannot be the default for its type.");

        return value;
    }

    public static T ThrowIfDefault<T>(
        this T value,
        string message,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new ArgumentOutOfRangeException(parameterName, value, message);

        return value;
    }

    public static Guid ThrowIfEmpty(
        this Guid value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Guid cannot be empty.", parameterName);

        return value;
    }

    public static T ThrowIfFalse<T>(
        this T value,
        bool condition,
        string message,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!condition)
            throw new ArgumentException(message, parameterName);

        return value;
    }

    public static void ThrowIfFalse(
        this bool condition,
        string message,
        [CallerArgumentExpression(nameof(condition))] string? parameterName = null)
    {
        if (!condition)
            throw new ArgumentException(message, parameterName);
    }

    public static void ThrowIfTrue(
        this bool condition,
        string message,
        [CallerArgumentExpression(nameof(condition))] string? parameterName = null)
    {
        if (condition)
            throw new ArgumentException(message, parameterName);
    }

    public static TEnum ThrowIfNotDefined<TEnum>(
        this TEnum value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value))
            throw new ArgumentOutOfRangeException(parameterName, value, "The enum value is not defined.");

        return value;
    }

    public static Uri ThrowIfNullOrNotAbsolute(
        [NotNull] this Uri? uri,
        [CallerArgumentExpression(nameof(uri))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(uri, parameterName);
        if (!uri.IsAbsoluteUri)
            throw new ArgumentException("URI must be absolute.", parameterName);

        return uri;
    }

    public static DateTime ThrowIfKindNot(
        this DateTime value,
        DateTimeKind requiredKind,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.Kind != requiredKind)
            throw new ArgumentException($"DateTime must have kind '{requiredKind}'.", parameterName);

        return value;
    }

    public static DateTimeOffset ThrowIfOffsetNotZero(
        this DateTimeOffset value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.Offset != TimeSpan.Zero)
            throw new ArgumentException("DateTimeOffset must represent UTC (offset zero).", parameterName);

        return value;
    }

    public static TExpected ThrowIfNotOfType<TExpected>(
        this object? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where TExpected : class
    {
        if (value is not TExpected typed)
            throw new ArgumentException($"Value must be of type '{typeof(TExpected).FullName}'.", parameterName);

        return typed;
    }

    public static object ThrowIfOfType<TForbidden>(
        this object? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value is TForbidden)
            throw new ArgumentException($"Value must not be of type '{typeof(TForbidden).FullName}'.", parameterName);

        return value!;
    }
}

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FieldOps.BuildingBlocks.Guards;

public static partial class GuardAgainstExtensions
{
    public static string ThrowIfNullOrEmpty(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, parameterName);
        return value;
    }

    public static string ThrowIfNullOrWhiteSpace(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        return value;
    }

    public static string ThrowIfShorterThan(
        this string value,
        int minLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(value, parameterName);
        if (value.Length < minLength)
            throw new ArgumentOutOfRangeException(parameterName, value.Length, $"String length must be at least {minLength}.");

        return value;
    }

    public static string ThrowIfLongerThan(
        this string value,
        int maxLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(value, parameterName);
        if (value.Length > maxLength)
            throw new ArgumentOutOfRangeException(parameterName, value.Length, $"String length must not exceed {maxLength}.");

        return value;
    }

    public static string ThrowIfLengthNotInRange(
        this string value,
        int minLength,
        int maxLength,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(value, parameterName);
        if (minLength > maxLength)
            throw new ArgumentException("minLength cannot be greater than maxLength.", nameof(minLength));

        var len = value.Length;
        if (len < minLength || len > maxLength)
            throw new ArgumentOutOfRangeException(parameterName, len, $"String length must be between {minLength} and {maxLength} inclusive.");

        return value;
    }
}

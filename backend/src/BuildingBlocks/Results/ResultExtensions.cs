namespace FieldOps.BuildingBlocks.Results;

public static class ResultExtensions
{
    public static Result<T> ToResult<T>(this T value) => Result<T>.Success(value);

    public static Result<T> ToResult<T>(this Result result, Func<T> valueFactory)
    {
        if (result.IsFailure)
            return Result<T>.Failure(result.Errors);

        return Result<T>.Success(valueFactory());
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
    {
        if (result.IsFailure)
            return Result<TOut>.Failure(result.Errors);

        return Result<TOut>.Success(mapper(result.Value));
    }

    public static Result<TOut> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Result<TOut>> binder)
    {
        if (result.IsFailure)
            return Result<TOut>.Failure(result.Errors);

        return binder(result.Value);
    }

    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Task<Result<TOut>>> binderAsync)
    {
        if (result.IsFailure)
            return Result<TOut>.Failure(result.Errors);

        return await binderAsync(result.Value).ConfigureAwait(false);
    }

    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, Task<Result<TOut>>> binderAsync)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.BindAsync(binderAsync).ConfigureAwait(false);
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<IReadOnlyList<Error>, TOut> onFailure) =>
        result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Errors);

    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<IReadOnlyList<Error>, TOut> onFailure) =>
        result.IsSuccess ? onSuccess() : onFailure(result.Errors);

    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Error error)
    {
        if (result.IsFailure)
            return result;

        return predicate(result.Value) ? result : Result<T>.Failure(error);
    }

    public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value);

        return result;
    }

    public static Result Tap(this Result result, Action action)
    {
        if (result.IsSuccess)
            action();

        return result;
    }

    public static Result<T> OnFailure<T>(
        this Result<T> result,
        Action<IReadOnlyList<Error>> handler)
    {
        if (result.IsFailure)
            handler(result.Errors);

        return result;
    }

    public static Result OnFailure(this Result result, Action<IReadOnlyList<Error>> handler)
    {
        if (result.IsFailure)
            handler(result.Errors);

        return result;
    }

    public static async Task<Result<T>> OnFailureAsync<T>(
        this Task<Result<T>> resultTask,
        Func<IReadOnlyList<Error>, Task> handler)
    {
        var result = await resultTask.ConfigureAwait(false);
        if (result.IsFailure)
            await handler(result.Errors).ConfigureAwait(false);

        return result;
    }

    public static Result<TOut> Map<TIn, TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<IReadOnlyList<Error>, Result<TOut>> onFailure) =>
        result.IsSuccess ? Result<TOut>.Success(onSuccess()) : onFailure(result.Errors);

    public static Result Combine(params Result[] results)
    {
        var failures = results.Where(r => r.IsFailure).SelectMany(r => r.Errors).ToArray();
        if (failures.Length > 0)
            return Result.Failure(failures);

        return Result.Success();
    }

    public static Result<IReadOnlyList<T>> CombineAll<T>(params Result<T>[] results)
    {
        var values = new List<T>();
        var errors = new List<Error>();

        foreach (var r in results)
        {
            if (r.IsSuccess)
                values.Add(r.Value);
            else
                errors.AddRange(r.Errors);
        }

        return errors.Count > 0
            ? Result<IReadOnlyList<T>>.Failure(errors)
            : Result<IReadOnlyList<T>>.Success(values);
    }
}

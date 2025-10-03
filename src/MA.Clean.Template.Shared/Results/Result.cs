using System.Collections.ObjectModel;

namespace MA.Clean.Template.Shared.Results;

/// <summary>Single error item for rich error reporting.</summary>
public sealed class ResultError
{
    public string Code { get; init; } = "error";
    public string Message { get; init; } = "An error occurred.";
    public string? Target { get; init; }
    public string? TraceId { get; init; }
    public string? HelpLink { get; init; }

    public IReadOnlyList<ResultError> Details { get; init; } = Array.AsReadOnly(Array.Empty<ResultError>());
    public IReadOnlyDictionary<string, object?> Meta { get; init; } =
        new ReadOnlyDictionary<string, object?>(new Dictionary<string, object?>());

    public static ResultError Of(
        string code,
        string message,
        string? target = null,
        string? traceId = null,
        string? helpLink = null,
        IEnumerable<ResultError>? details = null,
        IDictionary<string, object?>? meta = null)
    {
        var det = (details ?? Array.Empty<ResultError>()).ToList().AsReadOnly();
        var md  = new ReadOnlyDictionary<string, object?>(meta ?? new Dictionary<string, object?>());
        return new ResultError
        {
            Code = code,
            Message = message,
            Target = target,
            TraceId = traceId,
            HelpLink = helpLink,
            Details = det,
            Meta = md
        };
    }
}

/// <summary>Non-generic result with rich errors list.</summary>
public class Result
{
    public bool Succeeded { get; protected set; }
    public int StatusCode { get; protected set; }
    public string? Error { get; protected set; }
    public IReadOnlyList<ResultError> Errors { get; protected set; } = Array.AsReadOnly(Array.Empty<ResultError>());

    protected Result(bool succeeded, int statusCode, string? error = null, IEnumerable<ResultError>? errors = null)
    {
        Succeeded = succeeded;
        StatusCode = statusCode;
        Error = error;

        if (errors is not null)
        {
            Errors = errors.ToList().AsReadOnly();
        }
        else if (!string.IsNullOrWhiteSpace(error))
        {
            Errors = new List<ResultError> { ResultError.Of("error", error) }.AsReadOnly();
        }
    }

    // Success
    public static Result Success()  => new(true, 200);
    public static Result NoContent() => new(true, 204);
    public static Result Created()  => new(true, 201);

    // Errors
    public static Result BadRequest(string message, IEnumerable<ResultError>? errors = null) => new(false, 400, message, errors);
    public static Result NotFound(string message, IEnumerable<ResultError>? errors = null)   => new(false, 404, message, errors);
    public static Result Unauthorized(string message, IEnumerable<ResultError>? errors = null)=> new(false, 401, message, errors);
    public static Result Forbidden(string message, IEnumerable<ResultError>? errors = null)   => new(false, 403, message, errors);
    public static Result Conflict(string message, IEnumerable<ResultError>? errors = null)    => new(false, 409, message, errors);
    public static Result Failure(string message, int statusCode = 500, IEnumerable<ResultError>? errors = null)
        => new(false, statusCode, message, errors);
}

/// <summary>Generic result payload with rich errors list.</summary>
public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool succeeded, int statusCode, T? value, string? error = null, IEnumerable<ResultError>? errors = null)
        : base(succeeded, statusCode, error, errors)
    {
        Value = value;
    }

    // Success
    public static Result<T> Success(T value) => new(true, 200, value);
    public static Result<T> Created(T value) => new(true, 201, value);
    public static new Result<T> NoContent()  => new(true, 204, default);

    // Errors
    public static new Result<T> BadRequest(string message, IEnumerable<ResultError>? errors = null)
        => new(false, 400, default, message, errors);
    public static new Result<T> NotFound(string message, IEnumerable<ResultError>? errors = null)
        => new(false, 404, default, message, errors);
    public static new Result<T> Unauthorized(string message, IEnumerable<ResultError>? errors = null)
        => new(false, 401, default, message, errors);
    public static new Result<T> Forbidden(string message, IEnumerable<ResultError>? errors = null)
        => new(false, 403, default, message, errors);
    public static new Result<T> Conflict(string message, IEnumerable<ResultError>? errors = null)
        => new(false, 409, default, message, errors);
    public static new Result<T> Failure(string message, int statusCode = 500, IEnumerable<ResultError>? errors = null)
        => new(false, statusCode, default, message, errors);
}

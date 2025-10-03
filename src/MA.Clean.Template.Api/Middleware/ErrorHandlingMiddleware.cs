using System.Net;
using MA.Clean.Template.Shared.Results;

namespace MA.Clean.Template.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            var error = ResultError.Of("unhandled_exception", ex.Message, traceId: ctx.TraceIdentifier);
            var result = Result.Failure("Internal server error", (int)HttpStatusCode.InternalServerError, new[] { error });
            ctx.Response.StatusCode = result.StatusCode;
            await ctx.Response.WriteAsJsonAsync(new {
                status = result.StatusCode,
                errors = result.Errors
            });
        }
    }
}

public static class ErrorHandlingExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlingMiddleware>();
}

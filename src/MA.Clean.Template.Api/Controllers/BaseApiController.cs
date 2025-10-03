using Microsoft.AspNetCore.Mvc;
using MA.Clean.Template.Shared.Results;

namespace MA.Clean.Template.Api.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult FromResult(Result result)
        => result.Succeeded
            ? StatusCode(result.StatusCode)
            : ProblemFromResult(result);

    protected IActionResult FromResult<T>(Result<T> result)
        => result.Succeeded
            ? StatusCode(result.StatusCode, result.Value)
            : ProblemFromResult(result);

    private ObjectResult ProblemFromResult(Result result)
    {
        var pd = new ProblemDetails
        {
            Status = result.StatusCode,
            Title = result.Errors.FirstOrDefault()?.Code ?? "error",
            Detail = result.Error
        };
        pd.Extensions["errors"] = result.Errors;
        return StatusCode(result.StatusCode, pd);
    }
}

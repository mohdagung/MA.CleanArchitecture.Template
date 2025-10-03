using Microsoft.AspNetCore.Mvc;
using MediatR;
using MA.Clean.Template.Api.Controllers;
using MA.Clean.Template.Application.Samples.Commands;
using MA.Clean.Template.Application.Samples.Queries;

namespace MA.Clean.Template.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/samples")]
public sealed class SamplesController : BaseApiController
{
    private readonly IMediator _mediator;
    public SamplesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => FromResult(await _mediator.Send(new GetSampleById.Query(id), ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSampleRequest req, CancellationToken ct)
        => FromResult(await _mediator.Send(new CreateSample.Command(req.Name), ct));
}

public sealed record CreateSampleRequest(string Name);
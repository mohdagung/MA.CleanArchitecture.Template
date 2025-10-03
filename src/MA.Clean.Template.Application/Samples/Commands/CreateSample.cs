using MediatR;
using MA.Clean.Template.Shared.Results;
using MA.Clean.Template.Domain.Entities;
using MA.Clean.Template.Application.Common.Abstractions;

namespace MA.Clean.Template.Application.Samples.Commands;

/// <summary>
/// Feature file style: CreateSample.Command + Handler.
/// </summary>
public static class CreateSample
{
    public sealed record Command(string Name) : IRequest<Result<Guid>>;

    public sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly IRepository<Sample, Guid> _repo;

        public Handler(IRepository<Sample, Guid> repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken ct)
        {
            var errors = new List<ResultError>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add(ResultError.Of("validation", "Name is required.", target: nameof(request.Name)));

            if (errors.Count > 0)
                return Result<Guid>.BadRequest("Validation failed.", errors);

            var entity = new Sample(request.Name);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return Result<Guid>.Created(entity.Id);
        }
    }
}

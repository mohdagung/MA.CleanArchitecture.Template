using MediatR;
using MA.Clean.Template.Shared.Results;
using MA.Clean.Template.Domain.Entities;
using MA.Clean.Template.Application.Common.Abstractions;

namespace MA.Clean.Template.Application.Samples.Queries;

/// <summary>
/// Feature file style: GetSampleById.Query + Handler.
/// </summary>
public static class GetSampleById
{
    public sealed record Query(Guid Id) : IRequest<Result<Sample>>;

    public sealed class Handler : IRequestHandler<Query, Result<Sample>>
    {
        private readonly IReadRepository<Sample, Guid> _repo;

        public Handler(IReadRepository<Sample, Guid> repo)
        {
            _repo = repo;
        }

        public async Task<Result<Sample>> Handle(Query request, CancellationToken ct)
        {
            var sample = await _repo.GetByIdAsync(request.Id, ct);
            if (sample is null)
            {
                var err = ResultError.Of("not_found", "Resource not found", target: nameof(Query.Id));
                return Result<Sample>.NotFound($"Sample {request.Id} not found.", new[] { err });
            }

            return Result<Sample>.Success(sample);
        }
    }
}

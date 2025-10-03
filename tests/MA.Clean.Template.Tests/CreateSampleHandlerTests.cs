using FluentAssertions;
using MA.Clean.Template.Application.Samples.Commands;
using MA.Clean.Template.Domain.Entities;
using MA.Clean.Template.Infrastructure.Persistence;
using MA.Clean.Template.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MA.Clean.Template.Tests;

public class CreateSampleHandlerTests
{
    [Fact]
    public async Task Create_Should_Return_Created_With_Id()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        await using var db = new AppDbContext(opts);
        var repo = new RepositoryBase<Sample, Guid>(db);
        var handler = new CreateSample.Handler(repo);

        var res = await handler.Handle(new CreateSample.Command("A"), CancellationToken.None);

        res.Succeeded.Should().BeTrue();
        res.StatusCode.Should().Be(201);
        res.Value.Should().NotBeEmpty();

        var stored = await db.Samples.FirstOrDefaultAsync();
        stored!.Name.Should().Be("A");
    }

    [Fact]
    public async Task Create_Should_BadRequest_When_Name_Empty()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        await using var db = new AppDbContext(opts);
        var repo = new RepositoryBase<Sample, Guid>(db);
        var handler = new CreateSample.Handler(repo);

        var res = await handler.Handle(new CreateSample.Command(""), CancellationToken.None);

        res.Succeeded.Should().BeFalse();
        res.StatusCode.Should().Be(400);
        res.Errors.Should().NotBeEmpty();
    }
}

using FluentAssertions;
using MA.Clean.Template.Shared.Results;
using Xunit;

namespace MA.Clean.Template.Tests;

public class ResultTests
{
    [Fact]
    public void Success_Should_Set_Succeeded_True()
    {
        var r = Result.Success();
        r.Succeeded.Should().BeTrue();
        r.StatusCode.Should().Be(200);
    }

    [Fact]
    public void BadRequest_Should_Contain_Errors_List()
    {
        var r = Result.BadRequest("Validation failed.", new [] {
            ResultError.Of("validation", "Name is required.", target: "Name")
        });

        r.Succeeded.Should().BeFalse();
        r.StatusCode.Should().Be(400);
        r.Errors.Should().HaveCount(1);
        r.Errors.First().Target.Should().Be("Name");
    }

    [Fact]
    public void Generic_NotFound_Should_Map_Error()
    {
        var r = Result<string>.NotFound("missing");
        r.Succeeded.Should().BeFalse();
        r.StatusCode.Should().Be(404);
        r.Errors.Should().HaveCount(1);
        r.Errors.First().Code.Should().Be("error");
    }
}

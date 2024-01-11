using AutoFixture;
using BbgEducation.Application.Authentication.Login;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgSessions.Create;
using BbgEducation.Application.BbgSessions.Update;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.BbgSessions.Create;
public class BbgSessionCreateCommandValidatorTests
{
    private readonly BbgSessionCreateCommandValidator _testing = new BbgSessionCreateCommandValidator();
    private readonly IFixture _fixture = new Fixture();
    private readonly DateOnly _startDateOnly = DateOnly.Parse("09/20/2023");
    private readonly DateOnly _endDateOnly = DateOnly.Parse("12/20/2023");   

    public BbgSessionCreateCommandValidatorTests()
    {
        
    }

    [Fact]
    public void Validate_Should_Pass_WhenFullValidInputIsProvided() {

        BbgSessionCreateCommand command = new BbgSessionCreateCommand(
            _fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(),
            _startDateOnly, _endDateOnly);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData("ab")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde1")]

    public void Validate_Should_Fail_WhenNameIsInvalid(string name) {

        BbgSessionCreateCommand command =
            new BbgSessionCreateCommand(_fixture.Create<int>(), name, _fixture.Create<string>(), _startDateOnly, _endDateOnly);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }   

    [Fact]
    public void Validate_Should_Fail_WhenProgramIdInvalid() {

        BbgSessionCreateCommand command =
            new BbgSessionCreateCommand(-1, _fixture.Create<string>(), _fixture.Create<string>(), _startDateOnly, _endDateOnly);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    [Theory]
    [MemberData(nameof(InvalidCreateSessionCommandDates))]
    public void Validate_ShouldFail_WhenDatesAreInvalid(BbgSessionCreateCommand command) {
        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    public static IEnumerable<object[]> InvalidCreateSessionCommandDates() {
        yield return new[] { BuildWithDates(DateOnly.MinValue, null) };
        yield return new[] { BuildWithDates(null, DateOnly.MinValue) };
        yield return new[] { BuildWithDates(DateOnly.MaxValue, null) };
        yield return new[] { BuildWithDates(null, DateOnly.MaxValue) };
        yield return new[] { BuildWithDates(DateOnly.Parse("09/20/2023").AddDays(365), DateOnly.Parse("12/20/2023")) };

    }

    private static BbgSessionCreateCommand BuildWithDates(DateOnly? start, DateOnly? end) {
        return new BbgSessionCreateCommand(
                    10, "name", "description", start ?? DateOnly.Parse("09/20/2023"), end ?? DateOnly.Parse("12/20/2023"));
    }
}

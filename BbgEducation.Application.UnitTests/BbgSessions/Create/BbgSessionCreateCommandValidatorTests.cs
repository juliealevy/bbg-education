using AutoFixture;
using BbgEducation.Application.Authentication.Login;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgSessions.Create;
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
    public void Validate_Should_Fail_WhenDatesAreMinValue() {

        BbgSessionCreateCommand command =
            new BbgSessionCreateCommand(_fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(), DateOnly.MinValue, DateOnly.MinValue);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    [Fact]
    public void Validate_Should_Fail_WhenDatesAreMaxValue() {

        BbgSessionCreateCommand command =
            new BbgSessionCreateCommand(_fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(), DateOnly.MaxValue, DateOnly.MaxValue);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    [Fact]
    public void Validate_Should_Fail_WhenStartDatesAfterEnd() {

        BbgSessionCreateCommand command =
            new BbgSessionCreateCommand(_fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(), _startDateOnly.AddDays(365), _endDateOnly);

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
}

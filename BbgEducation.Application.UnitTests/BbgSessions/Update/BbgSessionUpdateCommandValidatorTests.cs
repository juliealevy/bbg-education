using AutoFixture;
using BbgEducation.Application.BbgSessions.Create;
using BbgEducation.Application.BbgSessions.Update;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BbgEducation.Application.UnitTests.BbgSessions.Update;
public class BbgSessionUpdateCommandValidatorTests {
    private readonly BbgSessionUpdateCommandValidator _testing = new BbgSessionUpdateCommandValidator();
    private readonly IFixture _fixture = new Fixture();
    private readonly DateOnly _startDateOnly = DateOnly.Parse("09/20/2023");
    private readonly DateOnly _endDateOnly = DateOnly.Parse("12/20/2023");

    [Fact]
    public void Validate_ShouldPass_WhenFullValidInputIsProvided() {

        BbgSessionUpdateCommand command = new BbgSessionUpdateCommand(_fixture.Create<int>(),
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

    public void Validate_ShouldFail_WhenNameIsInvalid(string name) {

        BbgSessionUpdateCommand command = new BbgSessionUpdateCommand(_fixture.Create<int>(), _fixture.Create<int>(),
            name, _fixture.Create<string>(), _startDateOnly, _endDateOnly);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }    

    [Fact]
    public void Validate_ShouldFail_WhenProgramIdInvalid() {

        BbgSessionUpdateCommand command = new BbgSessionUpdateCommand(-1,
           _fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(),
           _startDateOnly, _endDateOnly);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    [Fact]
    public void Validate_ShouldFail_WhenSessionIdInvalid() {

        BbgSessionUpdateCommand command = new BbgSessionUpdateCommand(_fixture.Create<int>(), -1,
            _fixture.Create<string>(), _fixture.Create<string>(),
           _startDateOnly, _endDateOnly);

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    [Theory]
    [MemberData(nameof(InvalidUpdateSessionCommandDates))]
    public void Validate_ShouldFail_WhenDatesAreInvalid(BbgSessionUpdateCommand command) {        
        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);

    }

    public static IEnumerable<object[]> InvalidUpdateSessionCommandDates() {
        yield return new[] {BuildWithDates( DateOnly.MinValue, null) };
        yield return new[] { BuildWithDates(null, DateOnly.MinValue) };
        yield return new[] { BuildWithDates(DateOnly.MaxValue, null) };
        yield return new[] { BuildWithDates(null, DateOnly.MaxValue) };
        yield return new[] { BuildWithDates(DateOnly.Parse("09/20/2023").AddDays(365), DateOnly.Parse("12/20/2023")) };

    }

    private static BbgSessionUpdateCommand BuildWithDates(DateOnly? start, DateOnly? end) {
        return new BbgSessionUpdateCommand(
                    10, 20, "name", "description", start ?? DateOnly.Parse("09/20/2023"), end ?? DateOnly.Parse("12/20/2023"));
    }
}
      
   
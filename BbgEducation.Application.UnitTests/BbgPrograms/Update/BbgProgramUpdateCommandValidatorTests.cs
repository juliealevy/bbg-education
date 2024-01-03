using AutoFixture;
using BbgEducation.Application.BbgPrograms.Update;
using FluentAssertions;

namespace BbgEducation.Application.UnitTests.BbgPrograms.Update;
public class BbgProgramUpdateCommandValidatorTests
{
    private readonly BbgProgramUpdateCommandValidator _testing = new BbgProgramUpdateCommandValidator();
    private readonly IFixture _fixture = new Fixture();

    [Fact]
    public void Validate_Should_Pass_WhenFullValidInputIsProvided() {

        BbgProgramUpdateCommand command = _fixture.Create<BbgProgramUpdateCommand>();

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Fact]
    public void Validate_Should_Pass_WhenDescriptionIsEmpty() {

        BbgProgramUpdateCommand command = new BbgProgramUpdateCommand(123,"Testing", "");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Fact]
    public void Validate_Should_Fail_WhenIdIsInvalid() {

        BbgProgramUpdateCommand command = new BbgProgramUpdateCommand(-123, "Testing", "");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenNameIsEmpty() {

        BbgProgramUpdateCommand command = new BbgProgramUpdateCommand(123, "", "description");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenNameIsTooShort() {

        BbgProgramUpdateCommand command = new BbgProgramUpdateCommand(123, "Tes", "Testing123");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenNameIsTooLong() {

        BbgProgramUpdateCommand command =
            new BbgProgramUpdateCommand(123, "TestingTestingTestingTestingTabcestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTe",
                "Testing123");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenDescriptionIsTooLong() {

        BbgProgramUpdateCommand command =
            new BbgProgramUpdateCommand(123, "Testing",
                "TestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTeTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTeTestingTestingTestingTestingTestingTestingTestingTesting");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }
}

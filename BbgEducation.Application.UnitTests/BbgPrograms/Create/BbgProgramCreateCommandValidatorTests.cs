using BbgEducation.Application.BbgPrograms.Create;
using FluentAssertions;

namespace BbgEducation.Application.UnitTests.BbgPrograms.Create;
public class BbgProgramCreateCommandValidatorTests
{
    private readonly BbgProgramCreateCommandValidator _testing = new BbgProgramCreateCommandValidator();

    public BbgProgramCreateCommandValidatorTests()
    {

    }

    [Fact]
    public void Validate_Should_Pass_WhenFullValidInputIsProvided()
    {

        BbgProgramCreateCommand command = new BbgProgramCreateCommand("Testing", "Testing123");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Fact]
    public void Validate_Should_Pass_WhenDescriptionIsEmpty()
    {

        BbgProgramCreateCommand command = new BbgProgramCreateCommand("Testing", "");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Fact]
    public void Validate_Should_Fail_WhenNameIsEmpty()
    {

        BbgProgramCreateCommand command = new BbgProgramCreateCommand("", "Testing123");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenNameIsTooShort()
    {

        BbgProgramCreateCommand command = new BbgProgramCreateCommand("Tes", "Testing123");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenNameIsTooLong()
    {

        BbgProgramCreateCommand command =
            new BbgProgramCreateCommand("TestingTestingTestingTestingTabcestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTe",
                "Testing123");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }

    [Fact]
    public void Validate_Should_Fail_WhenDescriptionIsTooLong()
    {

        BbgProgramCreateCommand command =
            new BbgProgramCreateCommand("Testing",
                "TestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTeTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTestingTeTestingTestingTestingTestingTestingTestingTestingTesting");

        var result = _testing.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }
}



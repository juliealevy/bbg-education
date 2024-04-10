using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.Courses.Create;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.Courses.Create;
public class CourseCreationCommandValidatorTests
{
    private readonly AbstractValidator<CourseCreateCommand> _underTest;

    public CourseCreationCommandValidatorTests()
    {
        _underTest = new CourseCreateCommandValidator();
    }


    [Fact]
    public void Validate_Should_Pass_WhenFullValidInputIsProvided() {

        CourseCreateCommand command = new CourseCreateCommand("Testing", "Testing123", false);

        var result = _underTest.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("", "blah blah blah")]
    [InlineData("abc", "")]
    [InlineData("abc", "blah blah blah")]
    [InlineData("nametoolongnametoolongnametoolongnametoolongnametoolongnametoolongnametoolongnametoolongnametoolongnametoolong", "blah blah blah")]
    [InlineData("abcde", "too long description more than 255 characters. too long description more than 255 characters. too long description more than 255 characters. too long description more than 255 characters. too long description more than 255 characters.too long description more than 255 characters")]
    public void Validation_ShouldFail_WhenInputInvalid(String name, String description) {
        CourseCreateCommand command = new CourseCreateCommand(name, description, false);

        var result = _underTest.Validate(command);

        result.Should().NotBeNull();
        result.IsValid.Should().Be(false);
    }


}

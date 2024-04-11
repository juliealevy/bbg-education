using Application.IntegrationTests.Abstractions;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Application.Courses.Create;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Courses;
public class CreateCourseTests : BaseIntegrationTest
{
    public CreateCourseTests(IntegrationTestWebAppFactory webAppFactory) : base(webAppFactory) {

    }

    [Fact]
    public async Task Create_ShouldAddNewCourse_WhenValidCommand() {

        //arrange
        var command = new CourseCreateCommand("Test Course 1", "course 1 description", false);

        //act
        var result = await Sender.Send(command);

        //assert
        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();
        int newId = result.AsT0;
        newId.Should().BePositive();

        var newCourse = await CourseRepository.GetCourseByIdAsync(newId, default);
        newCourse.Should().NotBeNull();
        newCourse.course_id.Should().Be(newId);
        newCourse.course_name.Trim().Should().Be(command.Name);
        newCourse.is_public.Should().Be(command.IsPublic);

    }

    [Fact]
    public async Task Create_ShouldFail_WhenNameExists() {

        //arrange
        String courseName = "Course1";

        CourseRepository.AddCourse(courseName, "", false);
        var command = new CourseCreateCommand(courseName, "course 1 description", false);

        //act
        var result = await Sender.Send(command);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(NameExistsValidationFailed));

    }

    [Fact]
    public async Task Create_ShouldFail_WhenNameEmpty() {

        //arrange
        var command = new CourseCreateCommand("", "course 1 description", false);

        //act
        var result = await Sender.Send(command);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(2);
        result.AsT1.GetType().Should().Be(typeof(ValidationFailed));

    }
}

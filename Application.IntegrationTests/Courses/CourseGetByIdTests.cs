using Application.IntegrationTests.Abstractions;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.GetById;
using BbgEducation.Application.Courses.Common;
using BbgEducation.Application.Courses.Create;
using BbgEducation.Application.Courses.GetById;
using FluentAssertions;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Courses;
public class CourseGetByIdTests : BaseIntegrationTest
{
    public CourseGetByIdTests(IntegrationTestWebAppFactory webAppFactory) : base(webAppFactory) {
    }

    [Fact]
    public async Task GetById_ShouldReturnCourse_WhenIdExists() {
        //arrange
        var createCommand = new CourseCreateCommand("Course 1", "Description for Course 1", false);
        var createResult = await Sender.Send(createCommand);
        int newId = createResult.AsT0;

        var getQuery = new CourseGetByIdQuery(newId);

        //act
        var getResult = await Sender.Send(getQuery);

        //assert
        getResult.Should().NotBeNull();
        getResult.IsT0.Should().BeTrue();
        CourseResult course= getResult.AsT0;

        course.Should().NotBeNull();
        course.Name.Trim().Should().Be(createCommand.Name);
        course.Id.Should().Be(newId);

    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenIdNotExists() {
        //arrange
        int invalidId = 100;
        var getQuery = new CourseGetByIdQuery(invalidId);

        //act
        var result = await Sender.Send(getQuery);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Should().Be(new NotFound());

    }
}

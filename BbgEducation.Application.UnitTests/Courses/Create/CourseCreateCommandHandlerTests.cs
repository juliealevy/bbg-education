using AutoFixture;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Courses.Create;
using BbgEducation.Domain.CourseDomain;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.Courses.Create;
public class CourseCreateCommandHandlerTests
{
    private readonly CourseCreateCommandHandler _testing;
    private readonly ICourseRepository _courseRepository = Substitute.For<ICourseRepository>();
    private readonly IFixture _fixture = new Fixture();

    public CourseCreateCommandHandlerTests()
    {
        _testing = new CourseCreateCommandHandler(_courseRepository);
    }

    [Fact]
    public async void Handle_ShouldCreateNewCourse_WhenInputValid() {
        var command = _fixture.Create<CourseCreateCommand>();

        _courseRepository.CheckCourseNameExistsAsync(command.Name, default).Returns(false);
        var savedCourse = CourseEntity.Create(1, command.Name, command.Description, command.isPublic);
        _courseRepository.AddCourse(command.Name, command.Description, command.isPublic).Returns((int)savedCourse.course_id!);

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();

        result.IsT0.Should().BeTrue();
        int? T0Value = result.AsT0;
        T0Value.Should().NotBeNull();
        T0Value.Should().Be(savedCourse.course_id);
    }


}

using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Courses.Common;
using BbgEducation.Application.Courses.GetById;
using BbgEducation.Domain.CourseDomain;
using FluentAssertions;
using MediatR.NotificationPublishers;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.Courses.GetById;
public class CourseGetByIdQueryHandlerTests
{
    private readonly CourseGetByIdQueryHandler _underTest;
    private readonly ICourseRepository _courseRepository = Substitute.For<ICourseRepository>();

    public CourseGetByIdQueryHandlerTests()
    {
        _underTest = new CourseGetByIdQueryHandler(_courseRepository);
    }

    [Fact]
    public async Task GetById_ReturnsCourse_WhenIdExists() {

        //arrange
        CourseEntity returnCourse = CourseEntity.Create(1, "Course 1", "description for course 1", false);
        _courseRepository.GetCourseByIdAsync((int)returnCourse.course_id!, default).Returns(returnCourse);

        CourseGetByIdQuery command = new CourseGetByIdQuery((int)returnCourse.course_id);

        //act
        var result = await _underTest.Handle(command, default);

        //assert
        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();
        var courseResult = result.AsT0;
        courseResult.Should().NotBeNull();
        courseResult.id.Should().Be(returnCourse.course_id);
        courseResult.name.Should().Be(courseResult.name);
        courseResult.description.Should().Be(courseResult.description);
        courseResult.isPublic.Should().Be(courseResult.isPublic);
        
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenIdNotExists() {

        //arrange        
        int invalidId = 100;
        _courseRepository.GetCourseByIdAsync(invalidId, default).ReturnsNull();

        CourseGetByIdQuery command = new CourseGetByIdQuery(invalidId);

        //act
        var result = await _underTest.Handle(command, default);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Should().Be(new NotFound());
    }
}

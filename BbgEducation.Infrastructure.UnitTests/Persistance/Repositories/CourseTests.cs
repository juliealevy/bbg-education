using BbgEducation.Application.Common.Errors;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.CourseDomain;
using BbgEducation.Infrastructure.Persistance.Repositories;
using BbgEducation.Infrastructure.UnitTests.Persistance.Repositories.Common;
using FluentAssertions;

namespace BbgEducation.Infrastructure.UnitTests.Persistance.Repositories;
public class CourseTests : BaseRepositoryTest
{
    private ICourseRepository _underTest;
    public CourseTests(RepositoryTestDbContainerFactory factory) : base(factory) {
        _underTest = new CourseRepository(SQLConnectionFactory);
    }

    [Fact]
    public async void AddCourse_ShouldReturnNewId_WhenInputValid() {
        //arrange
        string testName = "Test Course 1";
        string testDescription = "test course description";        

        //act
        int newCourseId = _underTest.AddCourse(testName, testDescription, true);

        //assert
        CourseEntity fetched = await _underTest.GetCourseByIdAsync(newCourseId, default);
        newCourseId.Should().BePositive();
        fetched.Should().NotBeNull();  
        fetched.course_id.Should().Be(newCourseId); 

    }

    [Fact]
    public void AddCourse_ShouldThrowExcpetion_WhenNameExists() {
        //arrange
        string testName = "Test Course 1";
        string testDescription = "test course description";

        //act
        int newCourseId = _underTest.AddCourse(testName, testDescription, true);
        Action action = () => _underTest.AddCourse(testName, testDescription, true);

        //assert        
        action.Should().Throw<DBException>()
           .WithMessage("*UNIQUE KEY*");

    }


    [Fact]
    public async Task CheckNameExists_ShouldReturnTrue_WhenExists() {
        //arrange
        string testName = "Test Course 1";
        string testDescription = "test course description";
        bool isPublic = false;

        int newCourseId = _underTest.AddCourse(testName, testDescription, false);

        //act
        bool exists = await _underTest.CheckCourseNameExistsAsync(testName, default);

        //assert
        exists.Should().BeTrue();

    }

    [Fact]
    public async Task CheckNameExists_ShouldReturnFalse_WhenNotExists() {
        //arrange
        string testName = "Test Course 1";

        //act
        bool exists = await _underTest.CheckCourseNameExistsAsync(testName, default);

        //assert
        exists.Should().BeFalse();

    }

    [Fact]
    public async Task GetById_ShouldReturnCourse_WhenIdExists() {
        //arrange
        string testName = "Test Course 1";
        string testDescription = "test course description";
        bool isPublic = false;

        int newCourseId = _underTest.AddCourse(testName, testDescription, false);

        //act
        CourseEntity found = await _underTest.GetCourseByIdAsync(newCourseId, CancellationToken.None);

        //assert
        found.Should().NotBeNull();
        found.course_name.Trim().Should().Be(testName);
        found.is_public.Should().Be(isPublic);
        
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenIdNotExists() {
        //arrange
        int badId = 100;

        //act        
        CourseEntity found = await _underTest.GetCourseByIdAsync(badId, default);

        //assert
        found.Should().BeNull();

    }

}

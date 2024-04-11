using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Application.Courses.Create;
using MediatR;
using OneOf;

namespace BbgEducation.Application.UnitTests.Courses.Create;

public class CourseCreateCommandHandler : IRequestHandler<CourseCreateCommand, OneOf<int, ValidationFailed>>
{
    private ICourseRepository _courseRepository;

    public CourseCreateCommandHandler(ICourseRepository courseRepository) {
        _courseRepository = courseRepository;
    }

    public async Task<OneOf<int, ValidationFailed>> Handle(CourseCreateCommand request, CancellationToken cancellationToken) {

        var nameExists = await _courseRepository.CheckCourseNameExistsAsync(request.Name, cancellationToken);
        if (nameExists) {
            return new NameExistsValidationFailed("Course");
        }

        var newId = _courseRepository.AddCourse(request.Name, request.Description,request.IsPublic);

        return newId;

        



    }
}
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Courses.Common;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.Courses.GetById;
public class CourseGetByIdQueryHandler : IRequestHandler<CourseGetByIdQuery, OneOf<CourseResult, NotFound>>
{

    private readonly ICourseRepository _courseRepository;

    public CourseGetByIdQueryHandler(ICourseRepository courseRepository) {
        _courseRepository = courseRepository;
    }

    public async Task<OneOf<CourseResult, NotFound>> Handle(CourseGetByIdQuery request, CancellationToken cancellationToken) {

        var courseEntity = await _courseRepository.GetCourseByIdAsync(request.CourseId, cancellationToken);

        return courseEntity is null ?
            new NotFound() : new CourseResult(
                (int)courseEntity.course_id!,
                courseEntity.course_name,
                courseEntity.description,
                courseEntity.is_public);
    }
}

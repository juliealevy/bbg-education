using BbgEducation.Application.Courses.Common;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.Courses.GetById;
public record CourseGetByIdQuery(int CourseId): IRequest<OneOf<CourseResult, NotFound>>;


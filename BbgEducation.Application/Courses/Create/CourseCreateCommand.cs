using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;

namespace BbgEducation.Application.Courses.Create;

public record CourseCreateCommand(string Name,
    string Description, bool IsPublic) : IRequest<OneOf<int, ValidationFailed>>;

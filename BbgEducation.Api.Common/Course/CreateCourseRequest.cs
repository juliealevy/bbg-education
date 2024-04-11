namespace BbgEducation.Api.Common.Course;

public record CreateCourseRequest(
    string Name,
    string Description,
    bool IsPublic);
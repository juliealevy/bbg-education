using BbgEducation.Domain.Common;

namespace BbgEducation.Domain.CourseDomain;
public sealed class CourseEntity : Entity
{
    public int? course_id { get; private set; }
    public string course_name { get; private set; } = string.Empty;
    public string description { get; private set; } = string.Empty;

    public bool is_public { get; private set; } = true;

    private CourseEntity(int? id,
        string name,
        string description,
        bool is_public,
        DateTime createdDateTime,
        DateTime updatedDateTime) {
        this.course_id = id;
        this.course_name = name;
        this.is_public = is_public;
        this.description = description;
        this.created_datetime = createdDateTime;
        this.updated_datetime = updatedDateTime;
    }

    public static CourseEntity Create(int id, string name, string description, bool isPublic) {
        return new CourseEntity(
            id,
            name,
            description,
            isPublic,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public static CourseEntity CreateNew(string name, string description, bool isPublic) {
        return new CourseEntity(
            null,
            name,
            description,
            isPublic,            
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public override bool isNew() {
        return this.course_id == null || this.course_id <= 0;
    }

    private CourseEntity() { }

    public override bool Equals(object? obj) {
        return obj is CourseEntity entity && course_id.Equals(entity.course_id);
    }

    public override int GetHashCode() {
        return HashCode.Combine(course_id, course_name);
    }

    public static bool operator ==(CourseEntity left, CourseEntity right) {
        return (left is null && right is null) ||
            left!.Equals(right);
    }

    public static bool operator !=(CourseEntity left, CourseEntity right) {
        return
            (left is null && right is not null) ||
            (right is null && left is not null) ||
            !left!.Equals(right);
    }
}

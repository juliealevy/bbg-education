using BbgEducation.Domain.Common;

namespace BbgEducation.Domain.BbgProgramDomain;

public sealed class BbgProgram: Entity
{
    public int? program_id { get; set; }
    public string program_name { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;
    public override bool isNew() {
        return program_id == null || program_id <= 0;
    }

    private BbgProgram(
        int? program_id,
        string program_name,
        string description,
        DateTime createdDateTime,
        DateTime updatedDateTime
        ) {
        this.program_id = program_id;
        this.program_name = program_name;
        this.description = description;
        base.created_datetime = createdDateTime;
        base.updated_datetime = updatedDateTime;

    }  

    public static BbgProgram Create(
        int program_id,
        string program_name,
        string description) {

        var program = new BbgProgram(
            program_id, 
            program_name,
            description,
            DateTime.UtcNow,
            DateTime.UtcNow);

        return program;

    }

    public static BbgProgram CreateNew(        
        string program_name,
        string description) {

        var program = new BbgProgram(
            null,
            program_name,
            description,
            DateTime.UtcNow,
            DateTime.UtcNow);

        return program;

    }

    private BbgProgram() {

    }

    public override bool Equals(object? obj) {
        return obj is BbgProgram entity && program_id.Equals(entity.program_id);
    }

    public static bool operator ==(BbgProgram left, BbgProgram right) {
        return (left is null && right is null) ||
            left!.Equals(right);
    }

    public static bool operator !=(BbgProgram left, BbgProgram right) {
        return  
            (left is null && right is not null) ||
            (right is null && left is not null) ||
            !left!.Equals(right);
    }

    public override int GetHashCode() {
        return HashCode.Combine(program_id, program_name);
    }

    public bool Equals(BbgProgram? other) {
        return Equals((object?)other);
    }
}

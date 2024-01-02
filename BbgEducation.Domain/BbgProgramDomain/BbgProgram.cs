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
}

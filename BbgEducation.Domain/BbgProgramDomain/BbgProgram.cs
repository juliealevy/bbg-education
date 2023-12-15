namespace BbgEducation.Domain.BbgProgramDomain;

public sealed class BbgProgram
{
    public int? program_id { get; set; }
    public string program_name { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;
    public bool isNew() {
        return program_id == null || program_id <= 0;
    }

}

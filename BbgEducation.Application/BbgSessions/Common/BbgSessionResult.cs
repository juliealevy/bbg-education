namespace BbgEducation.Application.BbgSessions.Common;
public record BbgSessionResult(
    BbgSessionProgramResult Program,
    int Id,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate
)
{
    public BbgSessionResult(
        int programId,
        string programName,
        int id,
        string name,
        string description,
        DateOnly startDate,
        DateOnly endDate) :
        this(
            new BbgSessionProgramResult(programId, programName),
            id,
            name,
            description,
            startDate,
            endDate) {
    }
}


public record BbgSessionProgramResult(
    int ProgramId,
    string ProgramName
);
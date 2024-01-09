using BbgEducation.Application.BbgPrograms.Common;

namespace BbgEducation.Application.BbgSessions.Common;
public record BbgSessionResult(
    BbgProgramResult Program,
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
        string programDescription,
        int id,
        string name,
        string description,
        DateOnly startDate,
        DateOnly endDate) :
        this(
            new BbgProgramResult(programId, programName, programDescription),
            id,
            name,
            description,
            startDate,
            endDate) {
    }
}

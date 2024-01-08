namespace BbgEducation.Application.BbgSessions.Common;
public record BbgSessionResult(    
    int ProgramId,
    string ProgramName,
    int Id,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate);

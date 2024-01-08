namespace BbgEducation.Api.BbgSessions;

public record CreateBbgSessionRequest(    
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate);


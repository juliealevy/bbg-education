namespace BbgEducation.Api.BbgSessions;

public record BbgSessionRequest(    
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate);


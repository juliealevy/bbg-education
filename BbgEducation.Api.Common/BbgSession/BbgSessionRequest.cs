namespace BbgEducation.Api.Common.BbgSession;

public record BbgSessionRequest(
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate);


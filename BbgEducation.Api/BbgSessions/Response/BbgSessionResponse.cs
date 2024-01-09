using BbgEducation.Api.BbgPrograms.Response;
using BbgEducation.Api.Hal;

namespace BbgEducation.Api.BbgSessions.Response;

public class BbgSessionResponse : HalResponse
{
    public BbgProgramResponse Program { get; set; } = new BbgProgramResponse();

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

}



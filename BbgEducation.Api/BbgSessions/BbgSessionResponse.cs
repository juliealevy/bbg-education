using BbgEducation.Api.Hal;

namespace BbgEducation.Api.BbgSessions;

public class BbgSessionResponse: HalResponse
{
    public int Id { get; set; }
    public int ProgramId { get; set; }  
    public string ProgramName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set;}

}

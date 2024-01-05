using BbgEducation.Api.Hal;

namespace BbgEducation.Api.BbgPrograms;
public class BbgProgramResponse:HalResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;   
    public string Description { get; set; } = string.Empty;

}
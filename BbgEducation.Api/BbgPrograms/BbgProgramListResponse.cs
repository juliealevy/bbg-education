using BbgEducation.Api.Hal;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.BbgPrograms;

public class BbgProgramListResponse: HalResponse
{
    [JsonPropertyOrder(10)]
    public List<BbgProgramResponse> Programs { get; set; } = new();
}

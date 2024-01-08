using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Hal;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.BbgSessions;

public class BbgSessionListResponse: HalResponse
{
    [JsonPropertyOrder(10)]
    public List<BbgSessionResponse> Sessions { get; set; } = new();
}

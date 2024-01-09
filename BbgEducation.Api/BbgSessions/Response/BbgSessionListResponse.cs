using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Hal;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.BbgSessions.Response;

public class BbgSessionListResponse : HalResponse, IHalListResponse<BbgSessionResponse>
{
    [JsonPropertyOrder(10)]
    
    public List<BbgSessionResponse> Items { get; set; } = new();
}

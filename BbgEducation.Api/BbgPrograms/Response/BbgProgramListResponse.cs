using BbgEducation.Api.Hal;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.BbgPrograms.Response;

public class BbgProgramListResponse : HalResponse, IHalListResponse<BbgProgramResponse>
{
    [JsonPropertyOrder(10)]
    public List<BbgProgramResponse> Items { get; set; } = new();
}

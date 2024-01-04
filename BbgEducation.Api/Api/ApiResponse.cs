using BbgEducation.Api.Links;

namespace BbgEducation.Api.Api;

public class ApiResponse: HalResponse
{
    public string version { get; } = string.Empty;

    public ApiResponse(ApiRouteData selfRouteData, string apiVersion)
    {
        version = apiVersion;
        AddSelfLink(selfRouteData);
        
    }   
}





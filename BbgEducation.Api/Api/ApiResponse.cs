using BbgEducation.Api.Common.Routes;
using BbgEducation.Api.Hal;

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





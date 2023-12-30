using BbgEducation.Api.Links;

namespace BbgEducation.Api.Api;

public class ApiResponse
{
    public Api api { get; } = new();
    public string version { get; } = string.Empty;

    public ApiResponse(string apiVersion)
    {
        version = apiVersion;
    }
    public void AddLink(string rel, string href, string method, object emptyBody)
    {
        api._links.Add(new Link(rel, href, method, emptyBody));
    }

    public void AddLink(string rel, string href, string method)
    {
        api._links.Add(new Link(rel, href, method));
    }
}
public class Api
{
    public List<Link> _links { get; set; } = new();

}




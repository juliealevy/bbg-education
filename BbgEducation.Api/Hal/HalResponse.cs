using BbgEducation.Api.Api;

namespace BbgEducation.Api.Links;

public class HalResponse
{
    private Dictionary<string, List<Link>> __links = new Dictionary<string, List<Link>>();

    protected HalResponse() { }

    public Dictionary<string, List<Link>> _links 
    {
        get {
            return __links;
        }
        set {
            __links = value;
        }
    }

    public void AddLink(string rel, ApiRouteData routeData) {
        if (routeData is not null) {
            AddLink(rel, routeData, null);
        }
    }

    public void AddLink(string rel, ApiRouteData routeData, object body) {
        if (routeData is not null) {
            AddApiLink(rel, routeData.RouteTemplate!, routeData.HttpMethod, body);
        }
    }

    public void AddSelfLink(ApiRouteData routeData) {
        if (routeData is not null) {
            AddApiLink("self", routeData.RouteTemplate!, routeData.HttpMethod, null);
        }
    }   
    private void AddApiLink(string rel, string href, string? method, object? body) {
        if (!_links.ContainsKey(rel)) {
            __links.Add(rel, new List<Link>());
        }
        var link = new Link(rel, href, method, body);
        __links[rel].Add(link);
    }

}

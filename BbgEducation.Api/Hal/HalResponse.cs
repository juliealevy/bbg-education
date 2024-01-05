using BbgEducation.Api.Common.Routes;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.Hal;

public class HalResponse
{
    private Dictionary<string, List<Link>> __links = new Dictionary<string, List<Link>>();

    protected HalResponse() { }

    [JsonPropertyOrder(1)]
    public Dictionary<string, List<Link>> _links {
        get {
            return __links;
        }
        set {
            __links = value;
        }
    }      

    public void AddSelfLink(ApiRouteData routeData) {
        if (routeData is not null) {
            AddLink(new Link("self", routeData.RouteTemplate!, routeData.HttpMethod!));
        }
    }

    public void AddSelfLink(Link? link) {
        AddLink(link);
    }

    public void AddLink(Link? link) {
        if (link is null)
            return;

        if (!_links.ContainsKey(link.Rel)) {
            __links.Add(link.Rel, new List<Link>());
        }        
        __links[link.Rel].Add(link);
    }   

}

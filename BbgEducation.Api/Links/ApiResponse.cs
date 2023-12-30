using BbgEducation.Api.Authentication;
using Microsoft.OpenApi.Validations;
using System.Text;

namespace BbgEducation.Api.Links;

public class ApiResponse {
    public Api api { get; } = new();
    public string version { get; } = String.Empty;

    public ApiResponse(string apiVersion) {
        version = apiVersion;
    }
    public void AddLink(string rel, string href, string method, object emptyBody) {
        this.api._links.Add(new Link(rel, href, method, emptyBody));
    }

    public void AddLink(string rel, string href, string method) {
        this.api._links.Add(new Link(rel, href, method));
    }
}
public class Api
{
    public List<Link> _links { get; set; } = new();

}




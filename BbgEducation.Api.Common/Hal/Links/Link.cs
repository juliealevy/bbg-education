using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BbgEducation.Api.Common.Hal.Links;

public class Link
{

    public string Href { get; set; }
    public bool Templated { get; set; } = false;

    [JsonIgnore]
    public string Rel { get; set; }

    public string Method { get; set; }
    public object? Body { get; set; } = null;

    public Link(string rel, string href, string method) {
        Rel = rel;
        Href = href;
        Templated = hasTemplate(href);
        Method = method;
    }

    public Link(string rel, string href, string method, object? body) {
        Rel = rel;
        Href = href;
        Templated = hasTemplate(href);
        Method = method;
        Body = body;
    }

    private bool hasTemplate(string href) {
        if (href == null) {
            return false;
        }
        string templatePattern = "\\{.+\\}";
        Match match = Regex.Match(href, templatePattern);
        return match.Success;
    }

}


using BbgEducation.Api.Authentication;
using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Tavis.UriTemplates;

namespace BbgEducation.Api.Links;

public class Link
{

    public string Href { get; set; }
    public bool Templated { get; set; } = false;

    // Extension properties not directly found in HAL spec.
    [JsonIgnore]
    public string Rel { get; set; }

    public object? Body { get; set; } = null;

    public string Method { get; set; }


    public Link(string rel, string href, string method) : this(rel, href, method, null) { }


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
        string templatePattern = ("\\{.+\\}");
        Match match = Regex.Match(href, templatePattern);
        return match.Success;
    }

}


using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Routes;

namespace BbgEducation.Api.Minimal.Hal;

public class MinimalApiLinkGenerator :  IMinimalApiLinkGenerator
{
    private readonly LinkGenerator _linkGenerator;

    public MinimalApiLinkGenerator(LinkGenerator linkGenerator) {
        _linkGenerator = linkGenerator;
    }

    public Link? GetActionLink(HttpContext context, string linkRelation, string endPointName, object? values) {

        var link = _linkGenerator.GetPathByName(context, endPointName, values);
        if (link is null) return null;

        return new Link(linkRelation, link, context.Request.Method);
    }

    public Link? GetApiLink(string linkRelation, Type module, string endPointName, object? body) {
        //var routeData = _routeService.GetRouteData(module, endPointName);
        //if (routeData is null) return null;

        //return new Link(linkRelation, routeData.RouteTemplate, routeData.HttpMethod, body);
        throw new NotImplementedException();
    }

    public Link? GetSelfLink(HttpContext context) {
        return new Link(LinkRelations.SELF, context.Request.Path.Value!, context.Request.Method);
    }

    public ApiRouteData? GetSelfRouteData(HttpContext context) {
        throw new NotImplementedException();
    }
}

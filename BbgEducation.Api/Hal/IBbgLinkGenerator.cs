using BbgEducation.Api.Common.Routes;

namespace BbgEducation.Api.Hal;
public interface IBbgLinkGenerator
{
    Link? GetActionLink(HttpContext context, string linkRelation, Type controller, string actionName, object? values);
    Link? GetApiLink(string linkRelation, Type controller, string actionName, object? body);

    Link? GetSelfLink(HttpContext context);

    ApiRouteData? GetSelfRouteData(HttpContext context);
}
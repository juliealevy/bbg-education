using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Routes;

namespace BbgEducation.Api.Minimal.Hal;
public interface IMinimalApiLinkGenerator
{
    Link? GetActionLink(HttpContext context, string linkRelation, string endPointName, object? values);
    Link? GetApiLink(string linkRelation, Type module, string endPointName, object? body);
    Link? GetSelfLink(HttpContext context);
    ApiRouteData? GetSelfRouteData(HttpContext context);
}
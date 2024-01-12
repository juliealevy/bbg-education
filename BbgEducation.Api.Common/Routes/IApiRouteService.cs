namespace BbgEducation.Api.Common.Routes;

public interface IApiRouteService
{
    ApiRouteData? GetSelfRouteData(RouteValueDictionary routeValues);

    ApiRouteData? GetRouteData(Type controllerClass, string actionName);

}
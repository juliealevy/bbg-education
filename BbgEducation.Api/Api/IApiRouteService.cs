namespace BbgEducation.Api.Api;

public interface IApiRouteService
{
    ApiRouteData? GetSelfRouteData(RouteValueDictionary routeValues);

    ApiRouteData? GetRouteData(Type controllerClass, string methodName);
}
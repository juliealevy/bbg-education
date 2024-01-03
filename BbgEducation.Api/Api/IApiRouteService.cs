namespace BbgEducation.Api.Api;

public interface IApiRouteService
{
    ApiRouteData? GetRouteData(string controllerName, string methodName);

    ApiRouteData? GetRouteData(Type controllerClass, string methodName);
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BbgEducation.Api.Api;

public class ApiRouteService : IApiRouteService
{
    private Dictionary<string, List<ApiRouteData>> _routesByController = new();

    public ApiRouteService(IActionDescriptorCollectionProvider provider) {
        LoadRoutes(provider);
    }

    private void LoadRoutes(IActionDescriptorCollectionProvider provider) {

        _routesByController = provider.
           ActionDescriptors
           .Items
           .OfType<ControllerActionDescriptor>()
           .Select(a => new ApiRouteData(
               a.ControllerName,
               a.MethodInfo.Name,
               a.AttributeRouteInfo?.Template,
               string.Join(", ", a.ActionConstraints?.OfType<HttpMethodActionConstraint>().SingleOrDefault()?.HttpMethods ?? new string[] { "any" })
           ))
           .GroupBy(c => c.ControllerName)
           .ToDictionary(g => g.Key, g => g.ToList());
    }

    public ApiRouteData? GetRouteData(Type controllerClass, string methodName) {
        if (!CheckIsApiController(controllerClass)) {
            return null;
        }
        List<ApiRouteData> routeList;
        if (!_routesByController.TryGetValue(GetControllerNameFromClass(controllerClass), out routeList!)) {
            return null;
        }
        return routeList!.Where(m => m.ActionMethodName.Equals(methodName)).FirstOrDefault();
    }

    private bool CheckIsApiController(Type controllerClass) {
        var attributes = controllerClass.GetCustomAttributes(true).ToList();
        var isIt =  attributes.Where(a => a.GetType() == typeof(ApiControllerAttribute)).Any();
        return isIt;
    }

    private string GetControllerNameFromClass(Type controllerClass) {
        return controllerClass.Name.Replace("Controller", null);
    }

    public ApiRouteData? GetRouteData(string controllerName, string methodName) {

        List<ApiRouteData> routeList;
        if (!_routesByController.TryGetValue(controllerName, out routeList!)) {
            return null;
        }
        return routeList!.Where(m => m.ActionMethodName.Equals(methodName)).FirstOrDefault();
    }
}

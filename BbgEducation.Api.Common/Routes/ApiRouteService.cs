using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BbgEducation.Api.Common.Routes;

public class ApiRouteService : IApiRouteService
{
    private Dictionary<string, List<ApiRouteData>> _routesByController = new();
    private readonly IActionDescriptorCollectionProvider _provider;

    public ApiRouteService(IActionDescriptorCollectionProvider provider) {       
        _provider = provider;
    }

    private void LoadRoutes()
    {
        if (_routesByController.Count == 0) {
            _routesByController = _provider.
               ActionDescriptors
               .Items
               .OfType<ControllerActionDescriptor>()
               .Select(a => new ApiRouteData(
                   a.ControllerName,
                   a.MethodInfo.Name,
                   a.AttributeRouteInfo?.Template!,
                   string.Join(", ", a.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods ?? new string[] { "any" })
               ))
               .GroupBy(c => c.ModuleName)
               .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    public ApiRouteData? GetRouteData(Type controllerClass, string actionName)
    {
        LoadRoutes();
        if (!CheckIsApiController(controllerClass))
        {
            return null;
        }
        List<ApiRouteData> routeList;
        if (!_routesByController.TryGetValue(GetControllerNameFromType(controllerClass), out routeList!))
        {
            return null;
        }
        return routeList!.Where(m => m.ActionMethodName.Equals(actionName)).FirstOrDefault();
    }

    private bool CheckIsApiController(Type controllerClass)
    {
        var attributes = controllerClass.GetCustomAttributes(true).ToList();
        var isIt = attributes.Where(a => a.GetType() == typeof(ApiControllerAttribute)).Any();
        return isIt;
    }

    private string GetControllerNameFromType(Type controllerClass)
    {
        return controllerClass.Name.Replace("Controller", null);
    }

    public ApiRouteData? GetRouteData(string controllerName, string actionName)
    {

        List<ApiRouteData> routeList;
        if (!_routesByController.TryGetValue(controllerName, out routeList!))
        {
            return null;
        }
        return routeList!.Where(m => m.ActionMethodName.Equals(actionName)).FirstOrDefault();
    }


    public ApiRouteData? GetSelfRouteData(RouteValueDictionary routeValues)
    {
        if (routeValues == null || routeValues.Count < 2)
        {
            return null;
        }
        return GetRouteData(routeValues["controller"]?.ToString()!, routeValues["action"]?.ToString()!);
    }
}

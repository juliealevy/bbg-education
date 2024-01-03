using BbgEducation.Api.Authentication;
using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Api;

[Route(ApiRoutes.Root)]
[AllowAnonymous]
public class ApiRootController : ApiControllerBase
{
    private readonly IApiRouteService _routeService;
    public ApiRootController(IApiRouteService routeService) {
        _routeService = routeService;
    }


    [HttpGet]
    public IActionResult Get()
    {
        var apiResponse = new ApiResponse("0.0.1");  //TODO:  better versioning
               
        apiResponse.AddLink("self", _routeService.GetRouteData(typeof(ApiRootController), "Get")!);             

        apiResponse.AddLink("auth:register", _routeService.GetRouteData(typeof(AuthenticationController), "Register")!, new RegisterRequest("", "", "", ""));
        apiResponse.AddLink("auth:login", _routeService.GetRouteData(typeof(AuthenticationController), "Login")!, new LoginRequest("", ""));       

        apiResponse.AddLink("programs:get_all", _routeService.GetRouteData(typeof(BbgProgramController), "GetAllPrograms")!);
        apiResponse.AddLink("programs:get_by_id", _routeService.GetRouteData(typeof(BbgProgramController), "GetProgramById")!);
        apiResponse.AddLink("programs:create", _routeService.GetRouteData(typeof(BbgProgramController), "CreateProgram")!, new CreateBbgProgramRequest("", ""));
        apiResponse.AddLink("programs:update", _routeService.GetRouteData(typeof(BbgProgramController), "UpdateProgram")!, new UpdateBbgProgramRequest(123, "Updated Name", "Updated Description"));      

        return Ok(apiResponse);
    }

   
   
   
}

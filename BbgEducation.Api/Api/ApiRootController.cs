using BbgEducation.Api.Authentication;
using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Links;
using BbgEducation.Application.BbgPrograms.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;

namespace BbgEducation.Api.Api;

[Route(ApiRoutes.Root)]
[AllowAnonymous]
public class ApiRootController : Controller
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ApiRootController(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public IActionResult Get()
    {

        var apiResponse = new ApiResponse("0.0.1");

        //apiResponse.AddLink("auth:register", _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext!, action: "Register", controller: "Authentication", values: null)!, "POST");
        //apiResponse.AddLink("auth:login", _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext!, action: "Login", controller: "Authentication", values: null)!, "POST");
        //apiResponse.AddLink("programs:get", _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext!, action: "GetAllPrograms", controller: "BbgProgram", values: null)!, "GET");
        //var href = _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext!, action: "GetProgramById", controller: "BbgProgram", values: new { programId })!;

        apiResponse.AddLink("self", Request.Path.Value!, "GET");
        apiResponse.AddLink("auth:register", ApiRoutes.Authentication.Register, "POST", new RegisterRequest("", "", "", ""));
        apiResponse.AddLink("auth:login", ApiRoutes.Authentication.Login, "POST", new LoginRequest("", ""));

        apiResponse.AddLink("programs:get_all", ApiRoutes.Programs.GetAll, "GET");
        apiResponse.AddLink("programs:get_by_id", ApiRoutes.Programs.GetById, "GET");
        apiResponse.AddLink("programs:create", ApiRoutes.Programs.Create, "POST", new CreateBbgProgramRequest("", ""));
        apiResponse.AddLink("programs:update", ApiRoutes.Programs.Update, "PUT", new UpdateBbgProgramRequest(-123, "Updated Name", "Updated Description"));

        return Ok(apiResponse);
    }

}

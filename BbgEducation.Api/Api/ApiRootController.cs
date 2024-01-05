using BbgEducation.Api.Authentication;
using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Api;

[AllowAnonymous]
[Route("")]
public class ApiRootController : ApiControllerBase
{
    private readonly string _version = "0.0.1";  //TODO:   move out to real versioning service/scheme
    private readonly IBbgLinkGenerator _linkGenerator;

    public ApiRootController(IBbgLinkGenerator linkGenerator) {
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    [Produces("application/hal+json")]
    public IActionResult Get()
    {        
        var apiResponse = new ApiResponse(_linkGenerator.GetSelfRouteData(HttpContext)!, _version);

        
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.REGISTER, typeof(AuthenticationController), "Register", new RegisterRequest("", "", "", "")));        
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.LOGIN, typeof(AuthenticationController), "Login", new LoginRequest("", "")));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.LOGOUT, typeof(AuthenticationController), "Logout",null));

        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.GET_ALL, typeof(BbgProgramController), "GetAllPrograms", null));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.GET_BY_ID, typeof(BbgProgramController), "GetProgramById", null));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.CREATE, typeof(BbgProgramController), "CreateProgram", new CreateBbgProgramRequest("", "")));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.UPDATE, typeof(BbgProgramController), "UpdateProgram", new UpdateBbgProgramRequest(-123, "", "")));

        return Ok(apiResponse);
    }

   
   
   
}

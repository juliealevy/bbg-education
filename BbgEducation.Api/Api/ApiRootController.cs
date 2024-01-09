using BbgEducation.Api.Authentication;
using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.BbgSessions;
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

        
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.REGISTER, typeof(AuthenticationController), nameof(AuthenticationController.Register), 
            new RegisterRequest("", "", "", "")));        
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.LOGIN, typeof(AuthenticationController), nameof(AuthenticationController.Login), 
            new LoginRequest("", "")));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.LOGOUT, typeof(AuthenticationController), nameof(AuthenticationController.Logout), null));

        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.GET_ALL, typeof(BbgProgramController), nameof(BbgProgramController.GetAllPrograms), null));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.GET_BY_ID, typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), null));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.CREATE, typeof(BbgProgramController), nameof(BbgProgramController.CreateProgram), 
            new CreateBbgProgramRequest("", "")));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Program.UPDATE, typeof(BbgProgramController), nameof(BbgProgramController.UpdateProgram), 
            new UpdateBbgProgramRequest("", "")));

        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Session.CREATE, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession),
            new BbgSessionRequest("", "", DateOnly.Parse("09/13/2023"), DateOnly.Parse("12/20/2023"))));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Session.UPDATE, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.UpdateSession),
            new BbgSessionRequest("", "", DateOnly.Parse("09/13/2023"), DateOnly.Parse("12/20/2023"))));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Session.GET_BY_ID, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionById), null));
        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Session.GET_BY_PROGRAM_ID, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionsByProgramId), null));

        apiResponse.AddLink(_linkGenerator.GetApiLink(LinkRelations.Session.GET_ALL, 
            typeof(BbgSessionController), nameof(BbgSessionController.GetAllSessions), null));

        return Ok(apiResponse);
    }

   
   
   
}

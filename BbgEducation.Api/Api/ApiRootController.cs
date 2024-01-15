using BbgEducation.Api.Authentication;
using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.BbgSessions;
using BbgEducation.Api.Common.Authentication;
using BbgEducation.Api.Common.BbgProgram;
using BbgEducation.Api.Common.BbgSession;
using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
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
    private readonly IRepresentationFactory _representationFactory;

    public ApiRootController(IBbgLinkGenerator linkGenerator, IRepresentationFactory representationFactory) {
        _linkGenerator = linkGenerator;
        _representationFactory = representationFactory;
    }

    [HttpGet]
    [Produces("application/hal+json")]
    public IActionResult Get() {
        var representation = _representationFactory.NewRepresentation(HttpContext)
            .WithProperty("version", _version);

        representation
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.REGISTER, typeof(AuthenticationController), nameof(AuthenticationController.Register),
                new RegisterRequest("", "", "", ""))!)
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.LOGIN, typeof(AuthenticationController), nameof(AuthenticationController.Login),
                new LoginRequest("", ""))!)
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Authentication.LOGOUT, typeof(AuthenticationController), nameof(AuthenticationController.Logout), null)!);

        representation
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Program.GET_ALL, typeof(BbgProgramController), nameof(BbgProgramController.GetAllPrograms), null)!)
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Program.GET_BY_ID, typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), null)!)
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Program.CREATE, typeof(BbgProgramController), nameof(BbgProgramController.CreateProgram),
                new CreateBbgProgramRequest("", ""))!)
            .WithLink(_linkGenerator.GetApiLink(LinkRelations.Program.UPDATE, typeof(BbgProgramController), nameof(BbgProgramController.UpdateProgram),
                new UpdateBbgProgramRequest("", ""))!);


        representation
          .WithLink(_linkGenerator.GetApiLink(LinkRelations.Session.CREATE, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession),
                new BbgSessionRequest("", "", DateOnly.Parse("09/13/2023"), DateOnly.Parse("12/20/2023")))!)
          .WithLink(_linkGenerator.GetApiLink(LinkRelations.Session.UPDATE, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.UpdateSession),
                new BbgSessionRequest("", "", DateOnly.Parse("09/13/2023"), DateOnly.Parse("12/20/2023")))!)
          .WithLink(_linkGenerator.GetApiLink(LinkRelations.Session.GET_BY_ID, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionById), null)!)
          .WithLink(_linkGenerator.GetApiLink(LinkRelations.Session.GET_BY_PROGRAM_ID, typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionsByProgramId), null)!)
          .WithLink(_linkGenerator.GetApiLink(LinkRelations.Session.GET_ALL,
                typeof(BbgSessionController), nameof(BbgSessionController.GetAllSessions), null)!);

        return Ok(representation);
    }

   
   
   
}

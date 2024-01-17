using BbgEducation.Api.Common.BbgSession;
using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.Create;
using BbgEducation.Application.BbgSessions.GetById;
using BbgEducation.Application.BbgSessions.GetByProgramId;
using BbgEducation.Application.BbgSessions.Update;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgSessions;

[Route("programs/{programId}/sessions")]
public class BbgProgramSessionController : ApiControllerBase {
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IBbgLinkGenerator _linkGenerator;
    private readonly IRepresentationFactory _representationFactory;

    public BbgProgramSessionController(IMapper mapper, ISender mediator, IBbgLinkGenerator linkGenerator, IRepresentationFactory representationFactory) {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = linkGenerator;
        _representationFactory = representationFactory;
    }

    [HttpPost]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> CreateSession(
       BbgSessionRequest request, int programId, CancellationToken token) {

        var command = _mapper.Map<BbgSessionCreateCommand>((request, programId));
        var createResult = await _mediator.Send(command, token);

        return createResult.Match<IActionResult>(
            sessionId =>
            {
                var representation = BuildAddUpdateRepresentation(programId, sessionId);
                return CreatedAtAction(nameof(CreateSession), value: representation);
            },
            failed => BuildActionResult(failed)
            );
    }


    [HttpGet]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> GetSessionsByProgramId(
        int programId, CancellationToken token) {

        var query = new BbgSessionGetByProgramIdQuery(programId);
        var getResultData = await _mediator.Send(query,token);


        return getResultData.Match<IActionResult>(
           sessionList =>
           {              
               var representation = _representationFactory.NewRepresentation(HttpContext);
               sessionList.ForEach(p =>
               {
                   representation.WithRepresentation("sessions", BuildGetSessionRepresentation(p, true));
               });
               return Ok(representation);
           },
           failed => BuildActionResult(failed)
       );

       

    }


    [HttpGet("{sessionId}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> GetSessionById(
        int programId, int sessionId, CancellationToken token) {

        var query = new BbgSessionGetByIdQuery(programId, sessionId);
        var getResult = await _mediator.Send(query,token);

        return getResult.Match<IActionResult>(
            session =>
            {
                var respresentation = BuildGetSessionRepresentation(session);
                return Ok(respresentation);
            },
            failed => BuildActionResult(failed),
            _ => NotFound()
        );
    }

    [HttpPut("{sessionId}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> UpdateSession(
        int programId, int sessionId, BbgSessionRequest request, CancellationToken token) {

        var command = _mapper.Map<BbgSessionUpdateCommand>((request, programId, sessionId));
        var createResult = await _mediator.Send(command,token);

        return createResult.Match<IActionResult>(
            success =>
            {
                var representation = BuildAddUpdateRepresentation(programId, sessionId);
                return Ok(representation);
            },
            _ => NotFound(),
            failed => BuildActionResult(failed)
            );
    }

    private IRepresentation BuildGetSessionRepresentation(BbgSessionResult session, bool selfIsById = false) {
        IRepresentation? representation = null;

        if (selfIsById) {
            representation = _representationFactory.NewRepresentation(
                _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(BbgProgramSessionController), 
                    nameof(BbgProgramSessionController.GetSessionById), new { programId = session.Program.Id, sessionId = session.Id })!);
        }
        else {
            representation = _representationFactory.NewRepresentation(HttpContext);
        }
        representation.WithObject(session)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.UPDATE,
                typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.UpdateSession), new { programId = session.Program.Id, sessionId = session.Id })!);
            

        return representation;
    }

    private IRepresentation BuildAddUpdateRepresentation(int programId,int sessionId) {
        var representation = _representationFactory.NewRepresentation(HttpContext)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_BY_ID, 
                typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionById), new { programId, sessionId })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_BY_PROGRAM_ID, typeof(BbgProgramSessionController),
                    nameof(BbgProgramSessionController.GetSessionsByProgramId), new { programId })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_ALL, typeof(BbgSessionController),
                    nameof(BbgSessionController.GetAllSessions), null)!);

        return representation;
    }
}

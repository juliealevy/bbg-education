using BbgEducation.Api.Common;
using BbgEducation.Api.Hal.Links;
using BbgEducation.Api.Hal.Resources;
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

    public BbgProgramSessionController(IMapper mapper, ISender mediator, IBbgLinkGenerator linkGenerator) {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = linkGenerator;
    }

    [HttpPost]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> CreateSession(
       BbgSessionRequest request, int programId) {

        var command = _mapper.Map<BbgSessionCreateCommand>((request, programId));
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            session =>
            {
                var representation = BuildAddUpdateRepresentation(session);
                return CreatedAtAction(nameof(CreateSession), value: representation);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }


    [HttpGet]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> GetSessionsByProgramId(
        int programId) {

        var query = new BbgSessionGetByProgramIdQuery(programId);
        var getResultData = await _mediator.Send(query);


        return getResultData.Match<IActionResult>(
           sessionList =>
           {              
               var representation = RepresentationFactory.NewRepresentation(HttpContext);
               sessionList.ForEach(p =>
               {
                   representation.WithRepresentation("sessions", BuildGetSessionRepresentation(p, true));
               });
               return Ok(representation);
           },
           failed => BadRequest(BuildValidationProblem(failed.Errors)),
           _ => NotFound()
       );

       

    }


    [HttpGet("{sessionId}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> GetSessionById(
        int programId, int sessionId) {

        var query = new BbgSessionGetByIdQuery(programId, sessionId);
        var getResult = await _mediator.Send(query);

        return getResult.Match<IActionResult>(
            session =>
            {
                var respresentation = BuildGetSessionRepresentation(session);
                return Ok(respresentation);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors)),
            _ => NotFound()
        );
    }

    [HttpPut("{sessionId}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> UpdateSession(
        int programId, int sessionId, BbgSessionRequest request) {

        var command = _mapper.Map<BbgSessionUpdateCommand>((request, programId, sessionId));
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            session =>
            {
                var representation = BuildAddUpdateRepresentation(session);
                return Ok(representation);
            },
            _ => NotFound(),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    private Representation BuildGetSessionRepresentation(BbgSessionResult session, bool selfIsById = false) {
        Representation? representation = null;

        if (selfIsById) {
            representation = RepresentationFactory.NewRepresentation(
                _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(BbgProgramSessionController), 
                    nameof(BbgProgramSessionController.GetSessionById), new { programId = session.Program.Id, sessionId = session.Id })!);
        }
        else {
            representation = RepresentationFactory.NewRepresentation(HttpContext);
        }
        representation.WithObject(session)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.UPDATE,
                typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.UpdateSession), new { programId = session.Program.Id, sessionId = session.Id })!);
            

        return representation;
    }

    private Representation BuildAddUpdateRepresentation(BbgSessionResult session) {
        var representation = RepresentationFactory.NewRepresentation(HttpContext)
            .WithObject(session)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_BY_ID, 
                typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionById), new { programId = session.Program.Id, sessionId = session.Id })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_BY_PROGRAM_ID, typeof(BbgProgramSessionController),
                    nameof(BbgProgramSessionController.GetSessionsByProgramId), new { programId = session.Program.Id })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_ALL, typeof(BbgSessionController),
                    nameof(BbgSessionController.GetAllSessions), null)!);

        return representation;
    }
}

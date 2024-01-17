using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgSessions;

[Route("sessions")]
public class BbgSessionController: ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IBbgLinkGenerator _linkGenerator;
    private readonly IRepresentationFactory _representationFactory;

    public BbgSessionController(ISender mediator, IBbgLinkGenerator linkGenerator, IRepresentationFactory representationFactory) {
        _mediator = mediator;
        _linkGenerator = linkGenerator;
        _representationFactory = representationFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSessions(CancellationToken token) {
        var query = new BbgSessionGetAllQuery();
        var getResultData = await _mediator.Send(query,token);

        var representation = _representationFactory.NewRepresentation(HttpContext);
        return getResultData.Match<IActionResult>(
           sessions =>
           {
               sessions.ForEach(s =>
               {
                   representation.WithRepresentation("sessions", BuildGetSessionRepresentation(s));
               });
               return Ok(representation);
           });

    }

    private IRepresentation BuildGetSessionRepresentation(BbgSessionResult session) {

        var representation = _representationFactory.NewRepresentation(
                _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(BbgProgramSessionController),
                    nameof(BbgProgramSessionController.GetSessionById), new { programId = session.Program.Id, sessionId = session.Id })!)
                .WithObject(session)
                .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.UPDATE,
                    typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.UpdateSession), new { programId = session.Program.Id, sessionId = session.Id })!);

        return representation;
    }
}

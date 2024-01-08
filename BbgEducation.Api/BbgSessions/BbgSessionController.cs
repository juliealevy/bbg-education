using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgSessions.GetAll;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgSessions;

[Route("sessions")]
public class BbgSessionController: ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IBbgLinkGenerator _linkGenerator;

    public BbgSessionController(IMapper mapper, ISender mediator, IBbgLinkGenerator linkGenerator) {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSessions() {
        var query = new BbgSessionGetAllQuery();
        var getResult = await _mediator.Send(query);

        var sessionListResponse = new BbgSessionListResponse();
        sessionListResponse.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));

        var sessionList = _mapper.Map<List<BbgSessionResponse>>(getResult);

        foreach (var session in sessionList.OrderBy(s => s.ProgramName)) {
            session.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_BY_ID,
                  typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionById),
                  new { programId = session.ProgramId, sessionId = session.Id }));
        }

        sessionListResponse.Sessions = sessionList;

        return Ok(sessionListResponse);

    }
}

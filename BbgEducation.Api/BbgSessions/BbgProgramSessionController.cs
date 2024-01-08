using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgSessions.Create;
using BbgEducation.Application.BbgSessions.GetById;
using BbgEducation.Application.BbgSessions.GetByProgramId;
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
    public async Task<IActionResult> CreateSession(
       CreateBbgSessionRequest request, int programId) {

        var command = _mapper.Map<BbgSessionCreateCommand>((request, programId));
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            program => {
                var response = _mapper.Map<BbgSessionResponse>(createResult.Value);
                response.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));
                //AddGetLinks(response);

                return CreatedAtAction(nameof(CreateSession), value: response);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }


    [HttpGet]    
    public async Task<IActionResult> GetSessionsByProgramId(
        int programId) {

        var query = new BbgSessionGetByProgramIdQuery(programId);
        var getResult = await _mediator.Send(query);


        return getResult.Match<IActionResult>(
           session =>
           {
               var sessionListResponse = new BbgSessionListResponse();
               sessionListResponse.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));

               var sessionList = _mapper.Map<List<BbgSessionResponse>>(session);

               foreach (var item in sessionList.OrderBy(s => s.ProgramName)) {
                   item.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.GET_BY_ID,
                         typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.GetSessionById),
                         new { programId = item.ProgramId, sessionId = item.Id }));
               }
               sessionListResponse.Sessions = sessionList;
               return Ok(sessionListResponse);
           },
           failed => BadRequest(BuildValidationProblem(failed.Errors)),
           _ => NotFound()
       );

       

    }


    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetSessionById(
        int programId, int sessionId) {

        var query = new BbgSessionGetByIdQuery(programId, sessionId);
        var getResult = await _mediator.Send(query);

        return getResult.Match<IActionResult>(
            session =>
            {
                var response = _mapper.Map<BbgSessionResponse>(getResult.Value);
                response.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));
                return Ok(response);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors)),
            _ => NotFound()
        );
    }
}

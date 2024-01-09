using BbgEducation.Api.BbgSessions.Response;
using BbgEducation.Api.Common;
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
    private readonly IBbgResponseBuilder<BbgSessionResult, BbgSessionResponse> _responseBuilder;
    private readonly IBbgResponseBuilder<List<BbgSessionResult>, BbgSessionListResponse> _responseListBuilder;

    public BbgProgramSessionController(IMapper mapper, ISender mediator, IBbgResponseBuilder<BbgSessionResult, BbgSessionResponse> responseBuilder, IBbgResponseBuilder<List<BbgSessionResult>, BbgSessionListResponse> responseListBuilder) {
        _mapper = mapper;
        _mediator = mediator;
        _responseBuilder = responseBuilder;
        _responseListBuilder = responseListBuilder;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(
       BbgSessionRequest request, int programId) {

        var command = _mapper.Map<BbgSessionCreateCommand>((request, programId));
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            program =>
            {
                var response = _responseBuilder.Build(program, HttpContext, true, true, false);
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
           sessionList =>
           {
               var sessionListResponse = _responseListBuilder.Build(sessionList, HttpContext, false, false, false);
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
                var response = _responseBuilder.Build(session, HttpContext, true, false, true);
                return Ok(response);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors)),
            _ => NotFound()
        );
    }

    [HttpPut("{sessionId}")]
    public async Task<IActionResult> UpdateSession(
        int programId, int sessionId, BbgSessionRequest request) {

        var command = _mapper.Map<BbgSessionUpdateCommand>((request, programId, sessionId));
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            session =>
            {
                var response = _responseBuilder.Build(session, HttpContext, true, true, false);
                return Ok(response);
            },
            _ => NotFound(),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }
}

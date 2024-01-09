using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.BbgSessions.Response;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.GetAll;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgSessions;

[Route("sessions")]
public class BbgSessionController: ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IBbgResponseBuilder<List<BbgSessionResult>, BbgSessionListResponse> _responseListBuilder;

    public BbgSessionController(ISender mediator, IBbgResponseBuilder<List<BbgSessionResult>, BbgSessionListResponse> responseListBuilder) {
        _mediator = mediator;
        _responseListBuilder = responseListBuilder;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSessions() {
        var query = new BbgSessionGetAllQuery();
        var getResult = await _mediator.Send(query);

        var sessionListResponse = _responseListBuilder.Build(getResult, HttpContext, false, false, false);

        return Ok(sessionListResponse);

    }
}

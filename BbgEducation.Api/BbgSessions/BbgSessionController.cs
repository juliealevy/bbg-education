using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgSessions.Create;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgSessions;

[Route("programs/{programId}/session")]
public class BbgSessionController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IBbgLinkGenerator _linkGenerator;

    public BbgSessionController(IMapper mapper, ISender mediator, IBbgLinkGenerator linkGenerator) {
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
}

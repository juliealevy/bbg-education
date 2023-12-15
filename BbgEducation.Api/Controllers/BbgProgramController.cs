using BbgEducation.Application.BbgPrograms.Commands;
using BbgEducation.Contracts.BbgProgram;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Controllers;

[Route("programs")]
public class BbgProgramController: ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public BbgProgramController(IMapper mapper, ISender mediator) {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram(
        CreateBbgProgramRequest request) {

        var command = _mapper.Map<CreateBbgProgramCommand>((request));
        var createResult = await _mediator.Send(command);
        return Ok(_mapper.Map<BbgProgramResponse>(createResult));
        
    }
}

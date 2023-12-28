using BbgEducation.Api.Common;
using BbgEducation.Application.BbgPrograms.Commands;
using BbgEducation.Application.BbgPrograms.Queries;
using BbgEducation.Contracts.BbgProgram;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgPrograms;

[Route("programs")]
public class BbgProgramController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public BbgProgramController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("{programId}")]
    public async Task<IActionResult> GetProgramsById(
        string programId)
    {

        var query = new BbgProgramGetByIdQuery(programId);
        var getResult = await _mediator.Send(query);
        return getResult is null ? NotFound() : Ok(_mapper.Map<BbgProgramResponse>(getResult));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrograms()
    {
        var query = new BbgProgramGetAllQuery();
        var getResult = await _mediator.Send(query);
        return Ok(_mapper.Map<List<BbgProgramResponse>>(getResult));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram(
        CreateBbgProgramRequest request)
    {

        var command = _mapper.Map<BbgProgramCreateCommand>(request);
        var createResult = await _mediator.Send(command);
        return Ok(_mapper.Map<BbgProgramResponse>(createResult));

    }

    [HttpPut]
    public async Task<IActionResult> UpdateProgram(
       UpdateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramUpdateCommand>(request);
        var updateResult = await _mediator.Send(command);
        return Ok(_mapper.Map<BbgProgramResponse>(updateResult));

    }

}

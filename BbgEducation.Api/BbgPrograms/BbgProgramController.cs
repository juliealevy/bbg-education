using BbgEducation.Api.Api;
using BbgEducation.Api.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.GetAll;
using BbgEducation.Application.BbgPrograms.GetById;
using BbgEducation.Application.BbgPrograms.Update;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgPrograms;

public class BbgProgramController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public BbgProgramController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpGet(ApiRoutes.Programs.GetById)]
    public async Task<IActionResult> GetProgramById(
        int programId)
    {

        var query = new BbgProgramGetByIdQuery(programId);
        var getResult = await _mediator.Send(query);

        return getResult.Match<IActionResult>(
            program => Ok(_mapper.Map<BbgProgramResponse>(getResult.Value)),
                _ => NotFound()            
            );
           
    }

    [HttpGet(ApiRoutes.Programs.GetAll)]
    public async Task<IActionResult> GetAllPrograms()
    {
        var query = new BbgProgramGetAllQuery();
        var getResult = await _mediator.Send(query);

        return Ok(_mapper.Map<List<BbgProgramResponse>>(getResult));
    }

    [HttpPost(ApiRoutes.Programs.Create)]
    public async Task<IActionResult> CreateProgram(
        CreateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramCreateCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            program => CreatedAtAction(nameof(CreateProgram), value: _mapper.Map<BbgProgramResponse>(createResult.Value)),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    [HttpPut(ApiRoutes.Programs.Update)]
    public async Task<IActionResult> UpdateProgram(
       UpdateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramUpdateCommand>(request);
        var updateResult = await _mediator.Send(command);

        return updateResult.Match<IActionResult>(
            program => Ok(_mapper.Map<BbgProgramResponse>(updateResult.Value)),
            _ => NotFound(),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );

    }

}

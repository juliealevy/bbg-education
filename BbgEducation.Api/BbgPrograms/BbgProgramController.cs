using BbgEducation.Api.BbgPrograms.Response;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.GetAll;
using BbgEducation.Application.BbgPrograms.GetById;
using BbgEducation.Application.BbgPrograms.Update;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.BbgPrograms;

[Route("programs")]
public class BbgProgramController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    private readonly IBbgResponseBuilder<BbgProgramResult, BbgProgramResponse> _responseBuilder;
    private readonly IBbgResponseBuilder<List<BbgProgramResult>, BbgProgramListResponse> _responseListBuilder;

    public BbgProgramController(IMapper mapper, ISender mediator, IBbgResponseBuilder<BbgProgramResult, BbgProgramResponse> responseBuilder, IBbgResponseBuilder<List<BbgProgramResult>, BbgProgramListResponse> responseListBuilder) {
        _mapper = mapper;
        _mediator = mediator;
        _responseBuilder = responseBuilder;
        _responseListBuilder = responseListBuilder;
    }

    [HttpGet("{programId}")]
    public async Task<IActionResult> GetProgramById(
        int programId)
    {

        var query = new BbgProgramGetByIdQuery(programId);
        var getResult = await _mediator.Send(query);

        return getResult.Match<IActionResult>(
            program => {
                var response = _responseBuilder.Build(program, HttpContext, true, false, true);
                return Ok(response);
                },
                _ => NotFound()            
            );
           
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrograms()
    {
        var query = new BbgProgramGetAllQuery();
        var getResult = await _mediator.Send(query);

        var programListResponse = _responseListBuilder.Build(getResult, HttpContext,false, false, false);
       

        return Ok(programListResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram(
        CreateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramCreateCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            program =>
            {
                var response = _responseBuilder.Build(program, HttpContext, true, true, false);
                return CreatedAtAction(nameof(CreateProgram), value: response);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    [HttpPut("{programId}")]
    public async Task<IActionResult> UpdateProgram(int programId,
       UpdateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramUpdateCommand>((request, programId));
        var updateResult = await _mediator.Send(command);

        return updateResult.Match<IActionResult>(
            program =>
            {
                var response = _responseBuilder.Build(program, HttpContext, true, true, false);
                return Ok(response);
            },
            _ => NotFound(),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );

    } 

}

using BbgEducation.Api.BbgSessions;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal;
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
    private readonly IBbgLinkGenerator _linkGenerator;

    public BbgProgramController(IMapper mapper, ISender mediator, IBbgLinkGenerator linkGenerator)
    {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = linkGenerator;
    }
    
    [HttpGet("{programId}")]
    public async Task<IActionResult> GetProgramById(
        int programId)
    {

        var query = new BbgProgramGetByIdQuery(programId);
        var getResult = await _mediator.Send(query);

        return getResult.Match<IActionResult>(
            program => { 
                var response = _mapper.Map<BbgProgramResponse>(getResult.Value);
                response.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));
                response.AddLink(_linkGenerator.GetActionLink(HttpContext,LinkRelations.Program.UPDATE, 
                    typeof(BbgProgramController),nameof(BbgProgramController.UpdateProgram),new {programId=programId}));
                response.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.CREATE, 
                    typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession), new { programId = response.Id }));
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

        var programListResponse = new BbgProgramListResponse();
        programListResponse.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));

        var programList = _mapper.Map<List<BbgProgramResponse>>(getResult);
        programList.ForEach(p =>
        {
            p.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_BY_ID, 
                typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = p.Id }));          
        });
        programListResponse.Programs = programList;

        return Ok(programListResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram(
        CreateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramCreateCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            program => {
                var response = _mapper.Map<BbgProgramResponse>(createResult.Value);
                response.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));
                AddGetLinks(response);

                return CreatedAtAction(nameof(CreateProgram), value: response);
                },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProgram(
       UpdateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramUpdateCommand>(request);
        var updateResult = await _mediator.Send(command);

        return updateResult.Match<IActionResult>(
            program =>
            {
                var response = _mapper.Map<BbgProgramResponse>(updateResult.Value);
                response.AddSelfLink(_linkGenerator.GetSelfLink(HttpContext));
                AddGetLinks(response);
                return Ok(response);
            },
            _ => NotFound(),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );

    }

    private void AddGetLinks(BbgProgramResponse response) {
        response.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_BY_ID, typeof(BbgProgramController),
                    "GetProgramById", new { programId = response.Id }));
        response.AddLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_ALL, typeof(BbgProgramController),
                    "GetAllPrograms", null));
    }

}

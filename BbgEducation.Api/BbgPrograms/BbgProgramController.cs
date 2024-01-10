using BbgEducation.Api.BbgSessions;
using BbgEducation.Api.Common;
using BbgEducation.Api.Hal.Links;
using BbgEducation.Api.Hal.Resources;
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
    private readonly IBbgLinkGenerator _linkGenerator;

    public BbgProgramController(IMapper mapper, ISender mediator, IBbgLinkGenerator blogLinkGenerator) {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = blogLinkGenerator;
    }

    [HttpGet("{programId}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> GetProgramById(
        int programId)
    {
        var query = new BbgProgramGetByIdQuery(programId);
        var getResult = await _mediator.Send(query);

        return getResult.Match<IActionResult>(
            program => {

                var representation = BuildGetProgramRepresentation(program);
                
                return Ok(representation);
                },
                _ => NotFound()            
            );
           
    }

    [HttpGet]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> GetAllPrograms()
    {
        var query = new BbgProgramGetAllQuery();
        var getResultData = await _mediator.Send(query);

        var representation = RepresentationFactory.NewRepresentation(HttpContext);
        getResultData.ForEach(p =>
        {
            representation.WithRepresentation("programs", BuildGetProgramRepresentation(p,true));
        });
      
        return Ok(representation);
    }

    [HttpPost]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> CreateProgram(
        CreateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramCreateCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match<IActionResult>(
            program =>
            {

                var response = BuildAddUpdateProgramRepresentation(program);
                
                //_responseBuilder.Build(program, HttpContext, true, true, false);
                return CreatedAtAction(nameof(CreateProgram), value: response);
            },
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );
    }

    [HttpPut("{programId}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> UpdateProgram(int programId,
       UpdateBbgProgramRequest request) {

        var command = _mapper.Map<BbgProgramUpdateCommand>((request, programId));
        var updateResult = await _mediator.Send(command);

        return updateResult.Match<IActionResult>(
            program =>
            {
                var response = BuildAddUpdateProgramRepresentation(program);
                //_responseBuilder.Build(program, HttpContext, true, true, false);
                return Ok(response);
            },
            _ => NotFound(),
            failed => BadRequest(BuildValidationProblem(failed.Errors))
            );

    }

    private Representation BuildGetProgramRepresentation(BbgProgramResult program, bool selfIsById = false) {

        Representation? representation = null;

        if (selfIsById) {
            representation = RepresentationFactory.NewRepresentation(
               _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = program.Id })!
           );
            
        }
        else {
            representation = RepresentationFactory.NewRepresentation(HttpContext);
        }

        representation.WithObject(program)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.UPDATE,
                   typeof(BbgProgramController), nameof(BbgProgramController.UpdateProgram), new { programId = program.Id })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.CREATE,
                    typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession), new { programId = program.Id })!);

        return representation;
    }

    private Representation BuildAddUpdateProgramRepresentation(BbgProgramResult program) {

        var representation = RepresentationFactory.NewRepresentation(HttpContext)
            .WithObject(program)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_BY_ID, 
                typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = program.Id })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_ALL, typeof(BbgProgramController),
                    nameof(BbgProgramController.GetAllPrograms), null)!);

        return representation;
    }

}

using BbgEducation.Api.BbgSessions;
using BbgEducation.Api.Common.BbgProgram;
using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
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
    private readonly IBbgLinkGenerator _linkGenerator;
    private readonly IRepresentationFactory _representationFactory;

    public BbgProgramController(IMapper mapper, ISender mediator, IBbgLinkGenerator blogLinkGenerator, IRepresentationFactory representationFactory) {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = blogLinkGenerator;
        _representationFactory = representationFactory;
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
    public async Task<IActionResult> GetAllPrograms() {
        var query = new BbgProgramGetAllQuery();
        var getResultData = await _mediator.Send(query);

        var representation = _representationFactory.NewRepresentation(HttpContext);
        return getResultData.Match<IActionResult>(
            programs =>
            {
                programs.ForEach(p =>
                {
                    representation.WithRepresentation("programs", BuildGetProgramRepresentation(p, true));
                });
                return Ok(representation);
            }
            );
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
                return CreatedAtAction(nameof(CreateProgram), value: response);
            },
            failed => BuildActionResult(failed)
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
            failed => BuildActionResult(failed)
            );

    }

    private IRepresentation BuildGetProgramRepresentation(BbgProgramResult program, bool selfIsById = false) {

        IRepresentation? representation = null;

        if (selfIsById) {
            representation = _representationFactory.NewRepresentation(
               _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = program.Id })!
           );
            
        }
        else {
            representation = _representationFactory.NewRepresentation(HttpContext);
        }

        representation.WithObject(program)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.UPDATE,
                   typeof(BbgProgramController), nameof(BbgProgramController.UpdateProgram), new { programId = program.Id })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.CREATE,
                    typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession), new { programId = program.Id })!);

        return representation;
    }

    private IRepresentation BuildAddUpdateProgramRepresentation(BbgProgramResult program) {

        var representation = _representationFactory.NewRepresentation(HttpContext)
            .WithObject(program)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_BY_ID, 
                typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = program.Id })!)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_ALL, typeof(BbgProgramController),
                    nameof(BbgProgramController.GetAllPrograms), null)!);

        return representation;
    }

}

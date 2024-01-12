using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.GetAll;
using Carter;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BbgEducation.Api.Minimal.BbgProgram;

public class BbgProgramResource : ICarterModule
{

    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IBbgLinkGenerator _linkGenerator;
    private readonly IRepresentationFactory _representationFactory;

    public BbgProgramResource(IMapper mapper, ISender mediator, IBbgLinkGenerator linkGenerator, IRepresentationFactory representationFactory) {
        _mapper = mapper;
        _mediator = mediator;
        _linkGenerator = linkGenerator;
        _representationFactory = representationFactory;
    }

    public void AddRoutes(IEndpointRouteBuilder app) {
        app.MapGet("/Programs", [Authorize] (HttpContext context) => GetAllPrograms)
            .WithName(nameof(GetAllPrograms));
        //app.MapGet("/Programs/{id}", [Authorize] (HttpContext context) =>  GetProgramById)
        //    .WithName(nameof(GetProgramById));
        //app.MapPost("/Programs", [Authorize] (HttpContext context) => AddProgram)
        //    .WithName(nameof(AddProgram));
        //app.MapPut("/Programs/{id}", [Authorize] (HttpContext context) => UpdateProgram)
        //    .WithName(nameof(UpdateProgram));
    }
   

    public async Task<IResult> GetAllPrograms(HttpContext context) {

        var query = new BbgProgramGetAllQuery();
        var getResultData = await _mediator.Send(query);

        var representation = _representationFactory.NewRepresentation(context);
        return getResultData.Match<IResult>(
            programs =>
            {
                programs.ForEach(p =>
                {
                   // representation.WithRepresentation("programs", BuildGetProgramRepresentation(p, true));
                });
                return Results.Ok(representation);
            }
            );
    }

    //private static async Task<IResult> GetProgramById(IBBGProgramRepository repo, int id) {

    //    try {
    //        var data = await repo.GetProgramById(id);
    //        if (data == null) return Results.NotFound();
    //        return Results.Ok(data);
    //    }
    //    catch (Exception ex) {
    //        return Results.Problem(title: $"Error retrieving program {id}", detail: ex.Message);
    //    }
    //}

    //private static async Task<IResult> AddProgram(IBBGProgramRepository repo, DataAccess.Models.BBGProgram entity) {
    //    try {
    //        await repo.AddProgram(entity);
    //        return Results.NoContent();
    //    }
    //    catch (Exception ex) {
    //        return Results.Problem(title: "Error adding program", detail: ex.Message);
    //    }
    //}

    //private static async Task<IResult> UpdateProgram(IBBGProgramRepository repo, DataAccess.Models.BBGProgram entity) {
    //    try {
    //        await repo.UpdateProgram(entity);
    //        return Results.NoContent();
    //    }
    //    catch (Exception ex) {
    //        return Results.Problem(title: "Error updating program", detail: ex.Message);
    //    }
    //}

    //private IRepresentation BuildGetProgramRepresentation(BbgProgramResult program, bool selfIsById = false) {

    //    IRepresentation? representation = null;

    //    if (selfIsById) {
    //        representation = _representationFactory.NewRepresentation(
    //           _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = program.Id })!
    //       );

    //    }
    //    else {
    //        representation = _representationFactory.NewRepresentation(HttpContext);
    //    }

    //    representation.WithObject(program)
    //        .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.UPDATE,
    //               typeof(BbgProgramController), nameof(BbgProgramController.UpdateProgram), new { programId = program.Id })!)
    //        .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Session.CREATE,
    //                typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession), new { programId = program.Id })!);

    //    return representation;
    //}

    //private IRepresentation BuildAddUpdateProgramRepresentation(BbgProgramResult program) {

    //    var representation = _representationFactory.NewRepresentation(HttpContext)
    //        .WithObject(program)
    //        .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_BY_ID,
    //            typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = program.Id })!)
    //        .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Program.GET_ALL, typeof(BbgProgramController),
    //                nameof(BbgProgramController.GetAllPrograms), null)!);

    //    return representation;
    //}
}

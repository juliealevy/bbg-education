using BbgEducation.Api.Common.Course;
using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Hal;
using BbgEducation.Application.Courses.Common;
using BbgEducation.Application.Courses.Create;
using BbgEducation.Application.Courses.GetById;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Courses;

[Route("courses")]
public class CourseController: ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IBbgLinkGenerator _linkGenerator;
    private readonly IRepresentationFactory _representationFactory;

    public CourseController(ISender mediator, IMapper mapper, IBbgLinkGenerator linkGenerator, IRepresentationFactory representationFactory) {
        _mediator = mediator;
        _mapper = mapper;
        _linkGenerator = linkGenerator;
        _representationFactory = representationFactory;
    }

    [HttpPost]    
    [Produces(RepresentationFactory.HAL_JSON)]
    public async Task<IActionResult> CreateCourse(
       CreateCourseRequest request, CancellationToken token) {

        var command = _mapper.Map<CourseCreateCommand>(request);
        var createResult = await _mediator.Send(command, token);

        return createResult.Match<IActionResult>(
            newId =>
            {
                var response = BuildAddUpdateCourseRepresentation(newId);
                return CreatedAtAction(nameof(CreateCourse), value: response);
            },
            failed => BuildActionResult(failed)
            );
    }

    [HttpGet("{id}")]
    [Produces(RepresentationFactory.HAL_JSON)]
    
    public async Task<IActionResult> GetCourseById(
         int id, CancellationToken token) {

        var query = new CourseGetByIdQuery(id);
        var result = await _mediator.Send(query, token);

        if (token.IsCancellationRequested) {
            return NoContent();
        }
        else {
            return result.Match<IActionResult>(
                course =>
                {
                    var representation = BuildGetCourseRepresentation(course);
                    return Ok(representation);                    
                },
                    _ => NotFound()
                );
        }

    }



    private IRepresentation BuildGetCourseRepresentation(CourseResult course, bool selfIsById = false) {
        IRepresentation? representation = null;

        if (selfIsById) {
            representation = _representationFactory.NewRepresentation(
               _linkGenerator.GetActionLink(HttpContext, LinkRelations.SELF, typeof(CourseController), nameof(CourseController.GetCourseById), new { id = course.Id })!
            );

        }
        else {
            representation = _representationFactory.NewRepresentation(HttpContext);
        }

        representation.WithObject(course);
            //.WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Course.UPDATE,
            //       typeof(CourseController), nameof(CourseController.UpdateProgram), new { programId = course.Id })!);

        return representation;
    }

    private IRepresentation BuildAddUpdateCourseRepresentation(int id) {

        var representation = _representationFactory.NewRepresentation(HttpContext)
            .WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Course.GET_BY_ID,
                typeof(CourseController), nameof(CourseController.GetCourseById), new { id = id })!);
            //.WithLink(_linkGenerator.GetActionLink(HttpContext, LinkRelations.Course.GET_ALL, typeof(CourseController),
            //        nameof(CourseController.GetAllCourses), null)!);

        return representation;
    }
}

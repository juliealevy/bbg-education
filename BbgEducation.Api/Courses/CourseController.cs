using BbgEducation.Api.Common.BbgProgram;
using BbgEducation.Api.Common.Course;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Application.BbgPrograms.Create;
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

    public CourseController(ISender mediator, IMapper mapper) {
        _mediator = mediator;
        _mapper = mapper;
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
                //var response = BuildAddUpdateProgramRepresentation(newId);
                return CreatedAtAction(nameof(CreateCourse), value: newId);
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
                    //var representation = BuildGetProgramRepresentation(program);
                    //return Ok(representation);
                    return Ok(course);
                },
                    _ => NotFound()
                );
        }

    }
}

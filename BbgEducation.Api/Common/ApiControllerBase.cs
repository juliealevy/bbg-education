using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BbgEducation.Api.Common;

[ApiController]
[Authorize]
public class ApiControllerBase : ControllerBase
{
    //protected IActionResult Problem(ValidationException ex)
    //{
    //    return ValidationProblem(statusCode: StatusCodes.Status400BadRequest, detail: ex.Message, title: "Validation Error");

    //}
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Errors;

public class ErrorsController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error() {

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;       

        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An unexpected error occurred.",
            detail: exception?.Message);


    }

   
}

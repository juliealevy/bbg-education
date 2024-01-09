using BbgEducation.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text;

namespace BbgEducation.Api.Errors;

/// <summary>
/// This handler is for unhandled exceptions not coming back to controllers as OneOf ValidationFailed
/// </summary>
public class ErrorsController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error() {

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;    
        
        if (exception is DBException dbEx) {          
            //for now just returning badrequest... ideally would use error codes to determine, etc.
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: dbEx.Title,
            detail: dbEx.Message);
        }

        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An unexpected error occurred.",
            detail: exception?.Message);


    }

   
}

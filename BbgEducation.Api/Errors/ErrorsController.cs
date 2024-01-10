using BbgEducation.Api.Hal.Resources;
using BbgEducation.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return HandleDBException(dbEx);
        }

        if (exception is HalRepresentationException hrEx) {
            return HandleHalException(hrEx);
        }

        return HandleUnknownException(exception!);


    }

    private IActionResult HandleDBException(DBException dbException) {       
        return Problem(statusCode: StatusCodes.Status400BadRequest, title: dbException.Title,
            detail: dbException.Message);
    }

    private IActionResult HandleHalException(HalRepresentationException halRepresentationException) {
        return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Error building api response",
           detail: halRepresentationException.Message);
    }

    private IActionResult HandleUnknownException(Exception exception) {
        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An unexpected error occurred.",
            detail: exception?.Message);
    }
}

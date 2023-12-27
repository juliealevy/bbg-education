using BbgEducation.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace BbgEducation.Api.Errors;

public class ErrorsController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error() {

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is IValidationException validationException) {
            return ValidationProblem(validationException.Errors);
        }

        if (exception is IServiceException serviceException) {
            return Problem(statusCode: (int)serviceException.StatusCode, title: serviceException.ErrorMessage);
        }

        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An unexpected error occurred.",
            detail: exception?.Message);


    }

    private IActionResult ValidationProblem(List<ValidationFailure> errors) {
        if (errors is null || !errors.Any()) {
            return Problem();
        }

        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors) {
            modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        return ValidationProblem(modelStateDictionary);
    }
}

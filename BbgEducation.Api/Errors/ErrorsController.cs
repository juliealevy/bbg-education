using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using BbgEducation.Application.Common.Interfaces.Exceptions;

namespace BbgEducation.Api.Errors;

public class ErrorsController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error() {

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is IValidatorException validatorException) {
            return BuildValidationProblem(validatorException.Errors);
        }

        if (exception is IApplicationException appException) {
            return Problem(statusCode: (int)appException.StatusCode, title: appException.ErrorMessage);
        }

        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An unexpected error occurred.",
            detail: exception?.Message);


    }

    private IActionResult BuildValidationProblem(List<ValidationFailure> errors) {
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

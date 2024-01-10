﻿using BbgEducation.Api.Common.Routes;
using BbgEducation.Api.Common.Routes.CustomAttributes;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BbgEducation.Api.Common;

[ApiController]
[Authorize]
[CustomRoutePrefix("api")]
public class ApiControllerBase : ControllerBase
{
    
    protected IActionResult BuildValidationProblem(IEnumerable<ValidationFailure> errors) {
        if (errors is null || !errors.Any()) {
            return Problem();
        }

        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors) {
            modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        return ValidationProblem(modelStateDictionary);
    }

    protected string ControllerName {
        get {
            return ControllerContext.RouteData.Values["controller"]!.ToString()!;
        }
    }
}

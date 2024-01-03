﻿using BbgEducation.Application.Authentication.Register;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Validation;
public record ValidationFailed(IEnumerable<ValidationFailure> Errors) {

    public ValidationFailed(ValidationFailure error): this(new [] { error }) {

    }

    public ValidationFailed(string propertyName, string message): this(
        new[] { new ValidationFailure(propertyName, message) }) {

    }
}
﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.Update;
public class BbgSessionUpdateCommandValidator: AbstractValidator<BbgSessionUpdateCommand>
{
    public BbgSessionUpdateCommandValidator()
    {
        RuleFor(x => x.ProgramId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.SessionId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(5);
        RuleFor(x => x.Description).MaximumLength(255);
        RuleFor(x => x.StartDate).NotEqual(DateOnly.MinValue).NotEqual(DateOnly.MaxValue);
        RuleFor(x => x.EndDate).NotEqual(DateOnly.MinValue).NotEqual(DateOnly.MaxValue);
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate);

    }
}

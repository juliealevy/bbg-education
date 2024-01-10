using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.Create;
public class BbgSessionCreateCommandValidator: AbstractValidator<BbgSessionCreateCommand>
{
    public BbgSessionCreateCommandValidator()
    {        
        RuleFor(x => x.ProgramId).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(5);
        RuleFor(x => x.Description).MaximumLength(255);
        RuleFor(x => x.StartDate).NotEqual(DateOnly.MinValue).NotEqual(DateOnly.MaxValue);
        RuleFor(x => x.EndDate).NotEqual(DateOnly.MinValue).NotEqual(DateOnly.MaxValue);
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate);

    }
}

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
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(5);
        RuleFor(x => x.Description).MaximumLength(255);
        RuleFor(x => x.StartDate).NotEmpty();  //check for min/max or default?
        RuleFor(x => x.EndDate).NotEmpty();    //check for min/max or default?
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate);

    }
}

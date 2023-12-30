using FluentValidation;

namespace BbgEducation.Application.BbgPrograms.Commands;
public class BbgProgramCreateCommandValidator: AbstractValidator<BbgProgramCreateCommand>
{
    public BbgProgramCreateCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(5);
        RuleFor(x => x.Description).MaximumLength(255);

        //TODO:  max and min lengths should come from domains, not hard coded here
    }
}

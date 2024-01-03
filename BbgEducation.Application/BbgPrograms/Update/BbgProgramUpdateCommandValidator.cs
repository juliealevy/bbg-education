using FluentValidation;

namespace BbgEducation.Application.BbgPrograms.Update;
public class BbgProgramUpdateCommandValidator : AbstractValidator<BbgProgramUpdateCommand>
{
    public BbgProgramUpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(5);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

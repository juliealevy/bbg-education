using MediatR;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using OneOf;
using FluentValidation.Results;
using FluentValidation;

namespace BbgEducation.Application.BbgPrograms.Create;
public class BbgProgramCreateCommandHandler : IRequestHandler<BbgProgramCreateCommand, OneOf<BbgProgramResult, ValidationFailed>>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;
    private readonly IValidator<BbgProgramCreateCommand> _validator;

    public BbgProgramCreateCommandHandler(IBbgProgramRepository bbgProgramRepository, IValidator<BbgProgramCreateCommand> validator)
    {
        _bbgProgramRepository = bbgProgramRepository;
        _validator = validator;
    }

    public async Task<OneOf<BbgProgramResult, ValidationFailed>> Handle(BbgProgramCreateCommand request, CancellationToken cancellationToken)
    {
        var validate = _validator.Validate(request);

        if (!validate.IsValid)
        {
            return new ValidationFailed(validate.Errors);
        }

        var programNameExists = await _bbgProgramRepository.CheckProgramNameExistsAsync(request.Name);

        if (programNameExists)
        {
            return new ValidationFailed(new ValidationFailure(nameof(BbgProgramCreateCommand.Name), "Name already exists"));
        }

        var program = BbgProgram.CreateNew(
            request.Name,
            request.Description);

        var newProgram = await _bbgProgramRepository.AddProgram(program);

        return new BbgProgramResult((int)newProgram.program_id!, newProgram.program_name, newProgram.description);

    }
}

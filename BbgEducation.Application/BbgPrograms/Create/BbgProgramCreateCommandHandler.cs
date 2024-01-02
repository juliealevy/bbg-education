using MediatR;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using OneOf;
using FluentValidation.Results;
using BbgEducation.Application.BbgPrograms.Common;

namespace BbgEducation.Application.BbgPrograms.Create;
public class BbgProgramCreateCommandHandler : IRequestHandler<BbgProgramCreateCommand, OneOf<BbgProgramResult, ValidationFailed>>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;

    public BbgProgramCreateCommandHandler(IBbgProgramRepository bbgProgramRepository)
    {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<OneOf<BbgProgramResult, ValidationFailed>> Handle(BbgProgramCreateCommand request, CancellationToken cancellationToken)
    {
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

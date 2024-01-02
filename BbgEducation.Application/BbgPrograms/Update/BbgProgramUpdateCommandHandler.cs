using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf.Types;
using OneOf;
using BbgEducation.Application.BbgPrograms.Common;

namespace BbgEducation.Application.BbgPrograms.Update;
public class BbgProgramUpdateCommandHandler : IRequestHandler<BbgProgramUpdateCommand, OneOf<BbgProgramResult, NotFound, ValidationFailed>>
{
    private readonly IBbgProgramRepository _programRepository;

    public BbgProgramUpdateCommandHandler(IBbgProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<OneOf<BbgProgramResult, NotFound, ValidationFailed>> Handle(BbgProgramUpdateCommand request, CancellationToken cancellationToken)
    {

        var program = BbgProgram.CreateExisting(
           request.Id,
           request.Name,
           request.Description);

        var programNameExists = await _programRepository.CheckProgramNameExistsAsync(request.Name);

        if (!programNameExists)
        {
            return new NotFound();
        }

        var newProgram = await _programRepository.AddProgram(program);

        return new BbgProgramResult((int)newProgram.program_id!, newProgram.program_name, newProgram.description);
    }
}

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
       
        var programExists = await _programRepository.GetProgramByIdAsync(request.Id);
        if (programExists is null) {
            return new NotFound();
        }

        //if the name changed, make sure it doesn't already exist
        if (!request.Name.Trim().Equals(programExists.program_name.Trim())) {
            var programNameExists = await _programRepository.CheckProgramNameExistsAsync(request.Name);

            if (programNameExists) {
                return new NameExistsValidationFailed("Program");
            }
        }
        var program = BbgProgram.Create(
          request.Id,
          request.Name,
          request.Description);

        var newProgram = await _programRepository.UpdateProgram(program);

        return new BbgProgramResult((int)newProgram.program_id!, newProgram.program_name, newProgram.description);
    }
}

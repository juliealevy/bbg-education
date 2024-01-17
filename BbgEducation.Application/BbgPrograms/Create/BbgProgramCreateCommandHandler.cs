using MediatR;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgPrograms.Create;
public class BbgProgramCreateCommandHandler : IRequestHandler<BbgProgramCreateCommand, OneOf<int, ValidationFailed>>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;

    public BbgProgramCreateCommandHandler(IBbgProgramRepository bbgProgramRepository)
    {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<OneOf<int, ValidationFailed>> Handle(BbgProgramCreateCommand request, CancellationToken cancellationToken)
    {
        var programNameExists = await _bbgProgramRepository.CheckProgramNameExistsAsync(request.Name, cancellationToken);

        if (programNameExists)
        {            
            return new NameExistsValidationFailed("Program");
        }

        var newId = _bbgProgramRepository.AddProgram(request.Name, request.Description);

        return newId;

    }
}

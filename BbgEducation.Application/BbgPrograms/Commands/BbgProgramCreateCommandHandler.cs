using MediatR;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Application.Common.Interfaces.Persistance;

namespace BbgEducation.Application.BbgPrograms.Commands;
public class BbgProgramCreateCommandHandler : IRequestHandler<BbgProgramCreateCommand, BbgProgram>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;

    public BbgProgramCreateCommandHandler(IBbgProgramRepository bbgProgramRepository) {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<BbgProgram> Handle(BbgProgramCreateCommand request, CancellationToken cancellationToken)
    {      
        var program = BbgProgram.CreateNew(             
            request.Name,
            request.Description);

        var newProgram = await _bbgProgramRepository.AddProgram(program);

        return newProgram;       
        
    }
}

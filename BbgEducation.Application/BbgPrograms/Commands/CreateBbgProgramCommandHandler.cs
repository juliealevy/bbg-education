using MediatR;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Application.Common.Interfaces.Persistance;

namespace BbgEducation.Application.BbgPrograms.Commands;
public class CreateBbgProgramCommandHandler : IRequestHandler<CreateBbgProgramCommand, BbgProgram>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;

    public CreateBbgProgramCommandHandler(IBbgProgramRepository bbgProgramRepository) {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<BbgProgram> Handle(CreateBbgProgramCommand request, CancellationToken cancellationToken)
    {      
        var program = new BbgProgram() {
            program_name = request.Name,
            description = request.Description,
        };

        await _bbgProgramRepository.AddProgram(program);

        return program;       
        
    }
}

using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf;

namespace BbgEducation.Application.BbgPrograms.GetAll;
public class BbgProgramGetAllQueryHandler : IRequestHandler<BbgProgramGetAllQuery, OneOf<List<BbgProgramResult>>>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;


    public BbgProgramGetAllQueryHandler(IBbgProgramRepository bbgProgramRepository)
    {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<OneOf<List<BbgProgramResult>>> Handle(BbgProgramGetAllQuery request, CancellationToken cancellationToken)
    {        
        if (cancellationToken.IsCancellationRequested) {
            return new List<BbgProgramResult>();
        }

        var programs = await _bbgProgramRepository.GetProgramsAsync(cancellationToken);
        

        return programs.Select(p =>
                new BbgProgramResult((int)p.program_id!, p.program_name, p.description)
            ).ToList();
    }
}

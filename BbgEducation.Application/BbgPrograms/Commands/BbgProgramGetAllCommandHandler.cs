using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgPrograms.Commands;
public class BbgProgramGetAllCommandHandler : IRequestHandler<BbgProgramGetAllCommand, List<BbgProgram>>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;

    public BbgProgramGetAllCommandHandler(IBbgProgramRepository bbgProgramRepository) {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<List<BbgProgram>> Handle(BbgProgramGetAllCommand request, CancellationToken cancellationToken) {

        var programs = await _bbgProgramRepository.GetProgramsAsync();
        return programs.ToList();
    }
}

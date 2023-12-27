using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgPrograms.Commands;
public class BbgProgramUpdateCommandHandler : IRequestHandler<BbgProgramUpdateCommand, BbgProgram>
{
    private readonly IBbgProgramRepository _programRepository;

    public BbgProgramUpdateCommandHandler(IBbgProgramRepository programRepository) {
        _programRepository = programRepository;
    }

    public async Task<BbgProgram> Handle(BbgProgramUpdateCommand request, CancellationToken cancellationToken) {
        var program = BbgProgram.CreateExisting(
            Int32.Parse(request.Id),
            request.Name,
            request.Description);

        var newProgram = await _programRepository.AddProgram(program);

        return newProgram;
    }
}

using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgPrograms.Commands;
public class BbgProgramGetByIdCommandHandler : IRequestHandler<BbgProgramGetByIdCommand, BbgProgram>
{
    private readonly IBbgProgramRepository _repository;
public BbgProgramGetByIdCommandHandler(IBbgProgramRepository repository) {
        _repository = repository;
    }

    public async Task<BbgProgram> Handle(BbgProgramGetByIdCommand request, CancellationToken cancellationToken) {
        return await _repository.GetProgramByIdAsync(request.id);
    }
}

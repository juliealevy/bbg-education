using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgPrograms.Queries;
public class BbgProgramGetByIdQueryHandler : IRequestHandler<BbgProgramGetByIdQuery, BbgProgram>
{
    private readonly IBbgProgramRepository _repository;
    public BbgProgramGetByIdQueryHandler(IBbgProgramRepository repository)
    {
        _repository = repository;
    }

    public async Task<BbgProgram> Handle(BbgProgramGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetProgramByIdAsync(request.id);
    }
}

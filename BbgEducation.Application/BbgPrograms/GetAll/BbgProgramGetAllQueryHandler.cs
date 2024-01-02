﻿using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;

namespace BbgEducation.Application.BbgPrograms.GetAll;
public class BbgProgramGetAllQueryHandler : IRequestHandler<BbgProgramGetAllQuery, List<BbgProgramResult>>
{
    private readonly IBbgProgramRepository _bbgProgramRepository;


    public BbgProgramGetAllQueryHandler(IBbgProgramRepository bbgProgramRepository)
    {
        _bbgProgramRepository = bbgProgramRepository;
    }

    public async Task<List<BbgProgramResult>> Handle(BbgProgramGetAllQuery request, CancellationToken cancellationToken)
    {

        var programs = await _bbgProgramRepository.GetProgramsAsync();        

        return programs.Select(p =>
                new BbgProgramResult((int)p.program_id!, p.program_name, p.description)
            ).ToList();
    }
}

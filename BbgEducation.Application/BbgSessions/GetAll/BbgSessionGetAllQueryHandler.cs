using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.Common.Interfaces.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.GetAll;
public class BbgSessionGetAllQueryHandler : IRequestHandler<BbgSessionGetAllQuery, List<BbgSessionResult>>
{
    private readonly IBbgSessionRepository _repository;

    public BbgSessionGetAllQueryHandler(IBbgSessionRepository repository) {
        _repository = repository;
    }

    public async Task<List<BbgSessionResult>> Handle(BbgSessionGetAllQuery request, CancellationToken cancellationToken) {
        var sessions =  await _repository.GetAllSessions(false);

        return sessions.Select(s =>
        {
            return new BbgSessionResult((int)s.session_program.program_id!, s.session_program.program_name,
                (int)s.session_id!, s.session_name, s.description, DateOnly.FromDateTime(s.start_date), DateOnly.FromDateTime(s.end_date));
        }).ToList();
    }
}

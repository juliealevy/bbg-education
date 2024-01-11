using BbgEducation.Application.BbgSessions.Common;
using MediatR;
using OneOf.Types;
using OneOf;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using MapsterMapper;

namespace BbgEducation.Application.BbgSessions.GetByProgramId;

public class BbgSessionGetByProgramIdQueryHandler : IRequestHandler<BbgSessionGetByProgramIdQuery, OneOf<List<BbgSessionResult>, ValidationFailed>>
{
    private readonly IBbgProgramRepository _programRepository;
    private readonly IBbgSessionRepository _sessionRepository;

    public BbgSessionGetByProgramIdQueryHandler(IBbgProgramRepository programRepository, IBbgSessionRepository sessionRepository) {
        _programRepository = programRepository;
        _sessionRepository = sessionRepository;       
    }

    public async Task<OneOf<List<BbgSessionResult>, ValidationFailed>> Handle(BbgSessionGetByProgramIdQuery request, CancellationToken cancellationToken) {

        
        var program = await _programRepository.GetProgramByIdAsync(request.ProgramId);
        if (program is null) {
            return new ProgramNotExistValidationFailed(request.ProgramId);
        }

        var sessions = await _sessionRepository.GetSessionsByProgramId(request.ProgramId);

        var sessionResults = sessions
            .Select(sr => new BbgSessionResult((int)sr.session_program.program_id!, sr.session_program.program_name, sr.session_program.description,
                (int)sr.session_id!, sr.session_name, sr.description, DateOnly.FromDateTime(sr.start_date), DateOnly.FromDateTime(sr.end_date)))
            .ToList();

        return sessionResults;
    }
}
using BbgEducation.Application.BbgSessions.Common;
using MediatR;
using OneOf.Types;
using OneOf;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;

namespace BbgEducation.Application.BbgSessions.GetById;
public class BbgSessionGetByProgramIdQueryHandler : IRequestHandler<BbgSessionGetByIdQuery, OneOf<BbgSessionResult, ValidationFailed, NotFound>>
{
    private readonly IBbgProgramRepository _programRepository;
    private readonly IBbgSessionRepository _sessionRepository;

    public BbgSessionGetByProgramIdQueryHandler(IBbgProgramRepository programRepository, IBbgSessionRepository sessionRepository) {
        _programRepository = programRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<OneOf<BbgSessionResult, ValidationFailed, NotFound>> Handle(BbgSessionGetByIdQuery request, CancellationToken cancellationToken) {

        var program = await _programRepository.GetProgramByIdAsync(request.ProgramId);
        if (program is null) {
            return new ProgramNotExistValidationFailed(request.ProgramId);
        }

        var session = await _sessionRepository.GetSessionById(request.SessionId);
        if (session is not null && session.session_program is not null &&
            session.session_program.program_id is not null && session.session_id is not null) {

            return new BbgSessionResult((int)session.session_program.program_id, session.session_program.program_name, session.session_program.description,
                (int)session.session_id, session.session_name, session.description, DateOnly.FromDateTime(session.start_date), DateOnly.FromDateTime(session.end_date));

        }

        return new NotFound();
    }
}

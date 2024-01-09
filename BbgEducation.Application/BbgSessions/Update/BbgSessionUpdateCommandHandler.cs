using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using MediatR;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.Update;
public class BbgSessionUpdateCommandHandler : IRequestHandler<BbgSessionUpdateCommand, OneOf<BbgSessionResult, NotFound, ValidationFailed>>
{
    private readonly IBbgSessionRepository _sessionRepository;

    public BbgSessionUpdateCommandHandler(IBbgSessionRepository repository) {
        _sessionRepository = repository;
    }

    public async Task<OneOf<BbgSessionResult, NotFound, ValidationFailed>> Handle(BbgSessionUpdateCommand request, CancellationToken cancellationToken) {

        var session = await _sessionRepository.GetSessionById(request.SessionId);
        if (session is null) {
            return new NotFound();
        }

        if (!request.ProgramId.Equals(session.session_program.program_id)) {
            //changing program, program must exist
        }
                
        if (!request.Name.Trim().Equals(session.session_name.Trim())) {
            var programNameExists = await _sessionRepository.CheckSessionNameExistsAsync(request.Name);

            if (programNameExists) {
                return new NameExistsValidationFailed("Session");
            }
        }

        var sessionToUpdate = BbgSession.Build(request.SessionId, request.Name, request.Description, request.StartDate.ToDateTime(TimeOnly.Parse("12:00 AM")), request.EndDate.ToDateTime(TimeOnly.Parse("12:00 AM")),
            BbgProgram.Create(request.ProgramId, "", ""), DateTime.UtcNow, DateTime.UtcNow);

        var updatedSession = await  _sessionRepository.UpdateSession(sessionToUpdate);

        var sessionProgramResult = new BbgProgramResult(
            (int)updatedSession.session_program.program_id!,
            updatedSession.session_program.program_name, updatedSession.session_program.description);

        return new BbgSessionResult(sessionProgramResult, (int)updatedSession.session_id!, updatedSession.session_name, updatedSession.description,
            DateOnly.FromDateTime(updatedSession.start_date), DateOnly.FromDateTime(updatedSession.end_date));

    }
}

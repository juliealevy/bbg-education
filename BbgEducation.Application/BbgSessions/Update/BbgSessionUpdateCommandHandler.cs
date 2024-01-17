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
public class BbgSessionUpdateCommandHandler : IRequestHandler<BbgSessionUpdateCommand, OneOf<Success, NotFound, ValidationFailed>>
{
    private readonly IBbgSessionRepository _sessionRepository;
    private readonly IBbgProgramRepository _programRepository;

    public BbgSessionUpdateCommandHandler(IBbgSessionRepository repository, IBbgProgramRepository programRepository) {
        _sessionRepository = repository;
        _programRepository = programRepository;
    }

    public async Task<OneOf<Success, NotFound, ValidationFailed>> Handle(BbgSessionUpdateCommand request, CancellationToken cancellationToken) {

        var session = await _sessionRepository.GetSessionByIdAsync(request.SessionId, cancellationToken);
        if (session is null) {
            return new NotFound();
        }

        //changing the program
        if (!request.ProgramId.Equals(session.session_program.program_id)) {
            var program = await _programRepository.GetProgramByIdAsync(request.ProgramId,cancellationToken); 
            if (program is null) {
                return new ProgramNotExistValidationFailed(request.ProgramId);
            }
        }
                
        //changing the name
        if (!request.Name.Trim().Equals(session.session_name.Trim())) {
            var programNameExists = await _sessionRepository.CheckSessionNameExistsAsync(request.Name,cancellationToken);

            if (programNameExists) {
                return new NameExistsValidationFailed("Session");
            }
        }

        //do the update
        var sessionToUpdate = BbgSession.Build(request.SessionId, request.Name, request.Description, request.StartDate.ToDateTime(TimeOnly.Parse("12:00 AM")), request.EndDate.ToDateTime(TimeOnly.Parse("12:00 AM")),
            BbgProgram.Create(request.ProgramId, "", ""));

        _sessionRepository.UpdateSession(sessionToUpdate);

        return new Success();

    }


}

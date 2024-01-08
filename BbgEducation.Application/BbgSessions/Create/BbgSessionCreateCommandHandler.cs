using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgSessionDomain;
using FluentValidation.Results;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.Create;
public class BbgSessionCreateCommandHandler : IRequestHandler<BbgSessionCreateCommand, OneOf<BbgSessionResult, ValidationFailed>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IBbgProgramRepository _programRepository;

    public BbgSessionCreateCommandHandler(ISessionRepository sessionRepository, IBbgProgramRepository programRepository) {
        _sessionRepository = sessionRepository;
        _programRepository = programRepository;
    }

    public async Task<OneOf<BbgSessionResult, ValidationFailed>> Handle(BbgSessionCreateCommand request, CancellationToken cancellationToken) {

        var sessionNameExists = await _sessionRepository.CheckSessionNameExistsAsync(request.Name);
        if (sessionNameExists) {
            return new NameExistsValidationFailed("Session");
        }
        
        var sessionProgram = await _programRepository.GetProgramByIdAsync(request.ProgramId);
        if (sessionProgram is null) {
            return new ValidationFailed(new ValidationFailure("ProgramId", "Program does not exist.", request.ProgramId));
        }

        var newSession = await _sessionRepository.AddSession(request.ProgramId, request.Name, request.Description,
            request.StartDate, request.EndDate);

        return new BbgSessionResult((int)newSession.session_program.program_id!, newSession.session_program.program_name, (int)newSession.session_id!, newSession.session_name, newSession.description,
            DateOnly.FromDateTime(newSession.start_date), DateOnly.FromDateTime(newSession.end_date));

    }
}

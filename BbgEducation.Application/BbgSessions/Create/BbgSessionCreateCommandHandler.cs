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
public class BbgSessionCreateCommandHandler : IRequestHandler<BbgSessionCreateCommand, OneOf<int, ValidationFailed>>
{
    private readonly IBbgSessionRepository _sessionRepository;
    private readonly IBbgProgramRepository _programRepository;

    public BbgSessionCreateCommandHandler(IBbgSessionRepository sessionRepository, IBbgProgramRepository programRepository) {
        _sessionRepository = sessionRepository;
        _programRepository = programRepository;
    }

    public async Task<OneOf<int, ValidationFailed>> Handle(BbgSessionCreateCommand request, CancellationToken cancellationToken) {

        var sessionNameExists = await _sessionRepository.CheckSessionNameExistsAsync(request.Name, cancellationToken);
        if (sessionNameExists) {
            return new NameExistsValidationFailed("Session");
        }
        
        var sessionProgram = await _programRepository.GetProgramByIdAsync(request.ProgramId,cancellationToken);
        if (sessionProgram is null) {
            return new ProgramNotExistValidationFailed(request.ProgramId);
        }

        var newSessionId = _sessionRepository.AddSession(request.ProgramId, request.Name, request.Description,
            request.StartDate, request.EndDate);

        return newSessionId;

    }
}

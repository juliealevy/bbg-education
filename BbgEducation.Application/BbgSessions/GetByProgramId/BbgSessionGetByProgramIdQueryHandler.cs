using BbgEducation.Application.BbgSessions.Common;
using MediatR;
using OneOf.Types;
using OneOf;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using MapsterMapper;

namespace BbgEducation.Application.BbgSessions.GetByProgramId;

public class BbgSessionGetByProgramIdQueryHandler : IRequestHandler<BbgSessionGetByProgramIdQuery, OneOf<List<BbgSessionResult>, ValidationFailed, NotFound>>
{
    private readonly IBbgProgramRepository _programRepository;
    private readonly IBbgSessionRepository _sessionRepository;
    private readonly IMapper _mapper;

    public BbgSessionGetByProgramIdQueryHandler(IBbgProgramRepository programRepository, IBbgSessionRepository sessionRepository) {
        _programRepository = programRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<OneOf<List<BbgSessionResult>, ValidationFailed, NotFound>> Handle(BbgSessionGetByProgramIdQuery request, CancellationToken cancellationToken) {

        var program = await _programRepository.GetProgramByIdAsync(request.ProgramId);
        if (program is null) {
            return new ProgramNotExistValidationFailed(request.ProgramId);
        }

        var sessions = await _sessionRepository.GetSessionsByProgramId(request.ProgramId);

        var sessionResults = _mapper.Map<List<BbgSessionResult>>(sessions);

        return sessionResults;
    }
}
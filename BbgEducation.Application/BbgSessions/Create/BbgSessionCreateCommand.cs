using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgSessions.Create;
public record BbgSessionCreateCommand(
    int ProgramId,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate) : IRequest<OneOf<int, ValidationFailed>>;

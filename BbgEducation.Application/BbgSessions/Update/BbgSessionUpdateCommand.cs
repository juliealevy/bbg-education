using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgSessions.Update;
public record BbgSessionUpdateCommand(
    int ProgramId,
    int SessionId,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate) : IRequest<OneOf<Success, NotFound, ValidationFailed>>;

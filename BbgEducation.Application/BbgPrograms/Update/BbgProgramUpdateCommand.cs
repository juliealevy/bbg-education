using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf.Types;
using OneOf;

namespace BbgEducation.Application.BbgPrograms.Update;
public record BbgProgramUpdateCommand(
    int Id,
    string Name,
    string Description) : IRequest<OneOf<BbgProgramResult, NotFound, ValidationFailed>>;


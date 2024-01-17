using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf.Types;
using OneOf;
using BbgEducation.Application.BbgPrograms.Common;

namespace BbgEducation.Application.BbgPrograms.Update;
public record BbgProgramUpdateCommand(
    int Id,
    string Name,
    string Description) : IRequest<OneOf<Success, NotFound, ValidationFailed>>;


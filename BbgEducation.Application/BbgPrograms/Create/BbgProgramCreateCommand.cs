using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgPrograms.Create;
public record BbgProgramCreateCommand(
    string Name,
    string Description) : IRequest<OneOf<int, ValidationFailed>>;

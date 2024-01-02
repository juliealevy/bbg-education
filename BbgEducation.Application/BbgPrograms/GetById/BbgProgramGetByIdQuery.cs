using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgPrograms.GetById;
public record BbgProgramGetByIdQuery(int Id) : IRequest<OneOf<BbgProgramResult, NotFound>>;

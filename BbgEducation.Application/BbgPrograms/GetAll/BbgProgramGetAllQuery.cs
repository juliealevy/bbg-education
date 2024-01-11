using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgPrograms.GetAll;
public record BbgProgramGetAllQuery() : IRequest<OneOf<List<BbgProgramResult>>>;

using BbgEducation.Domain.BbgProgramDomain;
using MediatR;

namespace BbgEducation.Application.BbgPrograms.GetAll;
public record BbgProgramGetAllQuery() : IRequest<List<BbgProgramResult>>;

using BbgEducation.Domain.BbgProgramDomain;
using MediatR;

namespace BbgEducation.Application.BbgPrograms.Commands;
public record BbgProgramCreateCommand(
    string Name,
    string Description) : IRequest<BbgProgram>;

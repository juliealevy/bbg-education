using BbgEducation.Domain.BbgProgramDomain;
using MediatR;

namespace BbgEducation.Application.BbgPrograms.Commands;
public record CreateBbgProgramCommand(
    string Name,
    string Description) : IRequest<BbgProgram>;

using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgPrograms.Queries;
public record BbgProgramGetByIdQuery(string id) : IRequest<BbgProgram>;

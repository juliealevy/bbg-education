using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.Update;
public record BbgSessionUpdateCommand(
    int ProgramId,
    int SessionId,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate) : IRequest<OneOf<BbgSessionResult, NotFound, ValidationFailed>>;

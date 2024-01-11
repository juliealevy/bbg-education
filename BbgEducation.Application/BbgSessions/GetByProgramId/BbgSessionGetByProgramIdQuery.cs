﻿using BbgEducation.Application.BbgPrograms.Common;
using MediatR;
using OneOf.Types;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.Common.Validation;

namespace BbgEducation.Application.BbgSessions.GetByProgramId;
public record BbgSessionGetByProgramIdQuery(int ProgramId) : IRequest<OneOf<List<BbgSessionResult>, ValidationFailed>>;


using BbgEducation.Application.BbgSessions.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.GetAll;
public record BbgSessionGetAllQuery: IRequest<List<BbgSessionResult>>;


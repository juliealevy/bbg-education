using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Domain.BbgSessionDomain;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.BbgSessions.Common;

public class BbgSessionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {
        config.NewConfig<BbgSession, BbgSessionResult>()
          .Map(dest => dest.Program, src =>
                new BbgSessionProgramResult((int)src.session_program.program_id!, src.session_program.program_name))
          .Map(dest => dest.Id, src => src.session_id)
          .Map(dest => dest.Name, src => src.session_name)
          .Map(dest => dest.Description, src => src.description)
          .Map(dest => dest.StartDate, src => DateOnly.FromDateTime(src.start_date))
          .Map(dest => dest.EndDate, src => DateOnly.FromDateTime(src.end_date));

    }
}

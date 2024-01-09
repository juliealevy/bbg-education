using BbgEducation.Api.BbgSessions.Response;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.Create;
using BbgEducation.Application.BbgSessions.Update;
using Mapster;

namespace BbgEducation.Api.BbgSessions;

public class BbgSessionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {
        config.NewConfig<(BbgSessionRequest request, int programId), BbgSessionCreateCommand>()
            .Map(dest => dest.ProgramId, src => src.programId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(BbgSessionRequest request, int programId, int sessionId), BbgSessionUpdateCommand>()
           .Map(dest => dest.ProgramId, src => src.programId)
           .Map(dest => dest.SessionId, src => src.sessionId)
           .Map(dest => dest, src => src.request);

        config.NewConfig<BbgSessionResult, BbgSessionResponse>()
           .Map(dest => dest.Program.Id, src => src.Program.Id)
           .Map(dest => dest.Program.Name, src => src.Program.Name)
           .Map(dest => dest.Program.Description, src => src.Program.Description);

    }
}

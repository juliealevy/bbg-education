using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.Create;
using Mapster;

namespace BbgEducation.Api.BbgSessions;

public class BbgSessionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {
        config.NewConfig<(CreateBbgSessionRequest request, int programId), BbgSessionCreateCommand>()
            .Map(dest => dest.ProgramId, src => src.programId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<BbgSessionResult, BbgSessionResponse>()
            .Map(dest => dest, src => src);
    }
}

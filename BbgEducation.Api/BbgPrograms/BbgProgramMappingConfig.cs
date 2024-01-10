using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.Update;
using Mapster;

namespace BbgEducation.Api.BbgPrograms;

public class BbgProgramMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {

        config.NewConfig<CreateBbgProgramRequest, BbgProgramCreateCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<(UpdateBbgProgramRequest request, int programId), BbgProgramUpdateCommand>()
            .Map(dest => dest.Id, src => src.programId)
            .Map(dest => dest, src => src.request);
    }

}

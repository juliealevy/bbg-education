using BbgEducation.Application.BbgPrograms.Commands;
using BbgEducation.Domain.BbgProgramDomain;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BbgEducation.Api.BbgPrograms;

public class BbgProgramMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {

        config.NewConfig<CreateBbgProgramRequest, BbgProgramCreateCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<BbgProgram, BbgProgramResponse>()
            .Map(dest => dest.Id, src => src.program_id)
            .Map(dest => dest.Name, src => src.program_name)
            .Map(dest => dest.Description, src => src.description);

    }

}

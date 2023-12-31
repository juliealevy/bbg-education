using BbgEducation.Application.BbgPrograms;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Domain.BbgProgramDomain;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BbgEducation.Api.BbgPrograms;

public class BbgProgramMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {

        config.NewConfig<CreateBbgProgramRequest, BbgProgramCreateCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<BbgProgramResult, BbgProgramResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

    }

}

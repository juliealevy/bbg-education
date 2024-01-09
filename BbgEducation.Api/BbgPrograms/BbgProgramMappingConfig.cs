using BbgEducation.Api.BbgPrograms.Response;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.Update;
using BbgEducation.Domain.BbgProgramDomain;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BbgEducation.Api.BbgPrograms;

public class BbgProgramMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) {

        config.NewConfig<CreateBbgProgramRequest, BbgProgramCreateCommand>()
            .Map(dest => dest, src => src);


        config.NewConfig<(UpdateBbgProgramRequest request, int programId), BbgProgramUpdateCommand>()
            .Map(dest => dest.Id, src => src.programId)
            .Map(dest => dest, src => src.request);


        config.NewConfig<BbgProgramResult, BbgProgramResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

    }

}

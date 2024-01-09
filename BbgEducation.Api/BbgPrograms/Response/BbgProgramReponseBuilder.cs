using BbgEducation.Api.BbgSessions;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgPrograms.Common;
using MapsterMapper;
using Microsoft.AspNetCore.Routing;

namespace BbgEducation.Api.BbgPrograms.Response;

public class BbgProgramReponseBuilder : BbgResponseBuilder<BbgProgramResult, BbgProgramResponse>
{

    public BbgProgramReponseBuilder(IMapper mapper, IBbgLinkGenerator linkGenerator) : base(mapper, linkGenerator)
    {

    }


    public override BbgProgramResponse? Build(BbgProgramResult resultData, HttpContext context, bool selfFromPath,
        bool addGetLinks, bool addCreateUpdateLinks)
    {

        if (resultData is not null)
        {
            var response = _mapper.Map<BbgProgramResponse>(resultData);
            if (selfFromPath)
            {
                response.AddSelfLink(_linkGenerator.GetSelfLink(context));
            }
            else
            {
                AddGetByIdSelfLink(response, context);
            }
            if (addGetLinks)
            {
                AddGetLinks(response, context);
            }
            if (addCreateUpdateLinks)
            {
                AddCreateUpdateLinks(response, context);
            }

            return response;
        }
        return null;
    }

    protected override void AddGetLinks(BbgProgramResponse response, HttpContext context)
    {
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Program.GET_BY_ID, typeof(BbgProgramController),
                    nameof(BbgProgramController.GetProgramById), new { programId = response.Id }));
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Program.GET_ALL, typeof(BbgProgramController),
                    nameof(BbgProgramController.GetAllPrograms), null));
    }

    protected override void AddCreateUpdateLinks(BbgProgramResponse response, HttpContext context)
    {
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Program.UPDATE,
           typeof(BbgProgramController), nameof(BbgProgramController.UpdateProgram), new { programId = response.Id }));
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Session.CREATE,
            typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.CreateSession), new { programId = response.Id }));
    }

    protected override void AddGetByIdSelfLink(BbgProgramResponse response, HttpContext context)
    {
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.SELF,
          typeof(BbgProgramController), nameof(BbgProgramController.GetProgramById), new { programId = response.Id }));
    }




}

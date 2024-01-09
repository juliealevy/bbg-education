using BbgEducation.Api.BbgPrograms;
using BbgEducation.Api.BbgPrograms.Response;
using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgSessions.Common;
using MapsterMapper;

namespace BbgEducation.Api.BbgSessions.Response;

public class BbgSessionResponseBuilder : BbgResponseBuilder<BbgSessionResult, BbgSessionResponse>
{
    private readonly IBbgResponseBuilder<BbgProgramResult,  BbgProgramResponse> _programResponseBuilder;
    public BbgSessionResponseBuilder(IMapper mapper, IBbgLinkGenerator linkGenerator, IBbgResponseBuilder<BbgProgramResult, BbgProgramResponse> programResponseBuilder) : base(mapper, linkGenerator) {
        _programResponseBuilder = programResponseBuilder;
    }

    public override BbgSessionResponse? Build(BbgSessionResult resultData, HttpContext context, bool selfFromPath, bool addGetLinks, bool addCreateUpdateLinks) {
        if (resultData is not null) {
            var programResponse = _programResponseBuilder.Build(resultData.Program, context, false, false, false);
            var response = _mapper.Map<BbgSessionResponse>(resultData);
            response.Program = programResponse!;

            if (selfFromPath) {
                response.AddSelfLink(_linkGenerator.GetSelfLink(context));
            }
            else {
                AddGetByIdSelfLink(response, context);
            }
            if (addGetLinks) {
                AddGetLinks(response, context);
            }
            if (addCreateUpdateLinks) {
                AddCreateUpdateLinks(response, context);
            }

            return response;
        }
        return null;
    }

    protected override void AddCreateUpdateLinks(BbgSessionResponse response, HttpContext context) {
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Session.UPDATE,
          typeof(BbgProgramSessionController), nameof(BbgProgramSessionController.UpdateSession), new { programId = response.Program.Id, sessionId = response.Id }));
       
    }

    protected override void AddGetByIdSelfLink(BbgSessionResponse response, HttpContext context) {
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.SELF, typeof(BbgProgramSessionController), 
            nameof(BbgProgramSessionController.GetSessionById), new { programId = response.Program.Id, sessionId = response.Id }));
    }

    protected override void AddGetLinks(BbgSessionResponse response, HttpContext context) {
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Session.GET_BY_ID, typeof(BbgProgramSessionController),
                    nameof(BbgProgramSessionController.GetSessionById), new { programId = response.Program.Id, sessionId = response.Id }));
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Session.GET_BY_PROGRAM_ID, typeof(BbgProgramSessionController),
                    nameof(BbgProgramSessionController.GetSessionsByProgramId), new { programId = response.Program.Id}));
        response.AddLink(_linkGenerator.GetActionLink(context, LinkRelations.Session.GET_ALL, typeof(BbgSessionController),
                    nameof(BbgSessionController.GetAllSessions), null));
    }
}

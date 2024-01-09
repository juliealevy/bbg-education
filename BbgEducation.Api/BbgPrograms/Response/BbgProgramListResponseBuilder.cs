using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgPrograms.Common;
using MapsterMapper;

namespace BbgEducation.Api.BbgPrograms.Response;

public class BbgProgramListResponseBuilder : BbgResponseBuilder<List<BbgProgramResult>, BbgProgramListResponse>
{
    private readonly IBbgResponseBuilder<BbgProgramResult, BbgProgramResponse> _itemResponseBuilder;

    public BbgProgramListResponseBuilder(IBbgResponseBuilder<BbgProgramResult, BbgProgramResponse> responseBuilder,
        IMapper mapper, IBbgLinkGenerator linkGenerator) : base(mapper, linkGenerator)
    {

        _itemResponseBuilder = responseBuilder;
    }

    public override BbgProgramListResponse? Build(List<BbgProgramResult> resultData, HttpContext context, bool selfFromPath, bool addGetLinks, bool addCreateUpdateLinks)
    {
        var programListResponse = new BbgProgramListResponse();
        programListResponse.AddSelfLink(_linkGenerator.GetSelfLink(context));

        resultData.ForEach(p =>
        {
            var response = _itemResponseBuilder.Build(p, context, false, false, true);
            if (response is not null)
            {
                programListResponse.Items.Add(response);
            }
        });

        return programListResponse;
    }   

    protected override void AddCreateUpdateLinks(BbgProgramListResponse response, HttpContext context) {
        
    }

    protected override void AddGetByIdSelfLink(BbgProgramListResponse response, HttpContext context) {
        
    }

    protected override void AddGetLinks(BbgProgramListResponse response, HttpContext context) {
        
    }
}

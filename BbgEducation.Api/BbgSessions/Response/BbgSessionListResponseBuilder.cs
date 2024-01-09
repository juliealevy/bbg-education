using BbgEducation.Api.Hal;
using BbgEducation.Application.BbgSessions.Common;
using MapsterMapper;

namespace BbgEducation.Api.BbgSessions.Response;

public class BbgSessionListResponseBuilder : BbgResponseBuilder<List<BbgSessionResult>, BbgSessionListResponse>
{
    private readonly IBbgResponseBuilder<BbgSessionResult, BbgSessionResponse> _itemResponseBuilder;

    public BbgSessionListResponseBuilder(IBbgResponseBuilder<BbgSessionResult, BbgSessionResponse> itemResponseBuilder, IMapper mapper, IBbgLinkGenerator linkGenerator)
        : base(mapper, linkGenerator) {
        _itemResponseBuilder = itemResponseBuilder;
    }  

    public override BbgSessionListResponse? Build(List<BbgSessionResult> resultData, HttpContext context, bool selfFromPath, bool addGetLinks, bool addCreateUpdateLinks) {
        var sessionListResponse = new BbgSessionListResponse();
        sessionListResponse.AddSelfLink(_linkGenerator.GetSelfLink(context));
        resultData.ForEach(s =>
        {
            var response = _itemResponseBuilder.Build(s, context, false, false, true);
            if (response is not null) {
                sessionListResponse.Items.Add(response);
            }
        });

        return sessionListResponse;
    }

    protected override void AddCreateUpdateLinks(BbgSessionListResponse response, HttpContext context) {
        
    }

    protected override void AddGetByIdSelfLink(BbgSessionListResponse response, HttpContext context) {
        
    }

    protected override void AddGetLinks(BbgSessionListResponse response, HttpContext context) {
        
    }
}
